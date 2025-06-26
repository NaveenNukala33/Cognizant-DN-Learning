CREATE DATABASE TopRatedProducts;
GO
USE TopRatedProducts;
GO

CREATE TABLE Products (
  ProductID   INT IDENTITY(1,1) PRIMARY KEY,
  ProductName NVARCHAR(100) NOT NULL,
  Category    NVARCHAR(50)  NOT NULL,
  Price       DECIMAL(10,2) NOT NULL
);
GO


INSERT INTO Products (ProductName, Category, Price) VALUES
  ('Zenith Noise Cancelling Headphones', 'Audio', 249.99),
  ('EchoSmart Speaker',                'Audio', 129.99),
  ('UltraBass Bluetooth Earbuds',      'Audio',  79.99),
  ('ProFit Treadmill',                 'Fitness',799.99),
  ('YogaFlex Mat',                     'Fitness', 59.99),
  ('HydroMax Water Bottle',            'Fitness', 24.99),
  ('EcoWood Dining Table',            'Furniture',699.99),
  ('ComfortZone Recliner',            'Furniture',499.99),
  ('OfficePro Ergonomic Chair',       'Furniture',199.99),
  ('Speedster Road Bike',             'Outdoor',999.99),
  ('TrailBlazer Hiking Boot',         'Outdoor',189.99),
  ('SunnyDay Umbrella',               'Outdoor', 39.99),
  ('GameMax Console X',               'Gaming', 499.99),
  ('RetroArcade Mini Console',        'Gaming', 129.99),
  ('PixelPro Gaming Mouse',           'Gaming',  79.99);
GO



-- Query-1:
SELECT
  ProductName,
  Category,
  Price,
  ROW_NUMBER() OVER (
    PARTITION BY Category
    ORDER BY Price DESC, ProductID
  ) AS SeqOrder
FROM Products
ORDER BY Category, SeqOrder;
GO

-- Query-2:
SELECT
  ProductName,
  Category,
  Price,
  RANK() OVER (
    PARTITION BY Category
    ORDER BY Price DESC
  ) AS RankWithGaps
FROM Products
ORDER BY Category, RankWithGaps;
GO
-- Query3:
SELECT
  ProductName,
  Category,
  Price,
  DENSE_RANK() OVER (
    PARTITION BY Category
    ORDER BY Price DESC
  ) AS DenseRankOrder
FROM Products
ORDER BY Category, DenseRankOrder;
GO

-- Query4: top 3 prices

WITH TopTierProducts AS (
  SELECT
    ProductName,
    Category,
    Price,
    DENSE_RANK() OVER (
      PARTITION BY Category
      ORDER BY Price DESC
    ) AS TierRank
  FROM Products
)
SELECT
  ProductName,
  Category,
  Price,
  TierRank
FROM TopTierProducts
WHERE TierRank <= 3
ORDER BY Category, TierRank;
GO
