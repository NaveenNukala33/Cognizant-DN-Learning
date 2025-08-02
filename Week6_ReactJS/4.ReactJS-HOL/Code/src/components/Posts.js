import React from 'react';
import Post from './Post';

class Posts extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      posts: [],
      hasError: false
    };
    console.log("Constructor called");
  }

  componentDidMount() {
    console.log("ComponentDidMount called");
    this.loadPosts();
  }

  loadPosts() {
    console.log("loadPosts started");
    fetch('https://jsonplaceholder.typicode.com/posts')
      .then(response => {
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        return response.json();
      })
      .then(data => {
        console.log("Posts fetched:", data.slice(0, 2));
        this.setState({ posts: data });
      })
      .catch(error => {
        console.error('Fetch failed:', error);
        // Fallback test data
        const testData = [
          { id: 1, title: "Fallback Title 1", body: "Fallback body 1" },
          { id: 2, title: "Fallback Title 2", body: "Fallback body 2" },
        ];
        this.setState({ posts: testData, hasError: true });
      });
  }

  render() {
    const { posts } = this.state;

    return (
      <div>
        <h1>Blog Posts</h1>
        {posts.length === 0 ? (
          <p>Loading posts...</p>
        ) : (
          posts.slice(0, 10).map(post => (
            <Post key={post.id} title={post.title} body={post.body} />
          ))
        )}
      </div>
    );
  }
}

export default Posts;