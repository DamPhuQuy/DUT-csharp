-- Seed categories
INSERT INTO categories (name) VALUES
    ('Electronics'),
    ('Clothing'),
    ('Food & Beverages'),
    ('Books'),
    ('Sports & Outdoors');

-- Seed products
INSERT INTO products (name, price, category_id) VALUES
    -- Electronics (category_id = 1)
    ('Wireless Bluetooth Headphones',  79.99, 1),
    ('4K Smart TV 55"',               499.99, 1),
    ('USB-C Laptop Charger',           29.99, 1),
    ('Mechanical Keyboard',            89.99, 1),
    ('Portable Power Bank 20000mAh',   39.99, 1),

    -- Clothing (category_id = 2)
    ('Men''s Running Jacket',          54.99, 2),
    ('Women''s Yoga Pants',            34.99, 2),
    ('Unisex Cotton Hoodie',           44.99, 2),
    ('Slim Fit Chinos',                39.99, 2),
    ('Waterproof Hiking Boots',        89.99, 2),

    -- Food & Beverages (category_id = 3)
    ('Organic Green Tea (50 bags)',     9.99, 3),
    ('Dark Roast Ground Coffee 500g',  14.99, 3),
    ('Mixed Nuts & Dried Fruits 1kg',  19.99, 3),
    ('Extra Virgin Olive Oil 750ml',   12.99, 3),
    ('Artisan Sourdough Bread',         5.99, 3),

    -- Books (category_id = 4)
    ('Clean Code – Robert C. Martin',  35.99, 4),
    ('The Pragmatic Programmer',       42.99, 4),
    ('Design Patterns (GoF)',          49.99, 4),
    ('C# in Depth – Jon Skeet',        44.99, 4),
    ('Refactoring – Martin Fowler',    38.99, 4),

    -- Sports & Outdoors (category_id = 5)
    ('Yoga Mat Non-Slip 6mm',          24.99, 5),
    ('Adjustable Dumbbell Set 20kg',  119.99, 5),
    ('Mountain Bike Helmet',           59.99, 5),
    ('Resistance Bands Set',           18.99, 5),
    ('Camping Tent 2-Person',          99.99, 5);
