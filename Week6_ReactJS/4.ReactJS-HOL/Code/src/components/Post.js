import React from 'react';

class Post extends React.Component {
  render() {
    return (
      <div style={{
        border: "1px solid gray",
        margin: "10px",
        padding: "10px",
        borderRadius: "8px",
        backgroundColor: "#f9f9f9"
      }}>
        <h3>{this.props.title}</h3>
        <p>{this.props.body}</p>
      </div>
    );
  }
}

export default Post;