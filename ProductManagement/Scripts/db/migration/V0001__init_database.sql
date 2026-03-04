CREATE DATABASE IF NOT EXISTS ProductManagement;
USE ProductManagement;

CREATE TABLE categories (
    id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(255) NOT NULL
);

CREATE TABLE products (
    id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(255) NOT NULL,
    price INT NOT NULL,
    category_id INT,
    FOREIGN KEY (category_id) REFERENCES categories(id)
);

-- Seed categories
INSERT INTO categories (name) VALUES
    ('Electronics'),
    ('Clothing'),
    ('Food & Beverages'),
    ('Books'),
    ('Sports & Outdoors');

-- Seed products
INSERT INTO products (name, price, category_id) VALUES
    ('Wireless Headphones', 79, 1),
    ('Smartphone 12 Pro', 999, 1),
    ('USB-C Hub', 35, 1),
    ('Bluetooth Speaker', 55, 1),
    ('Running T-Shirt', 25, 2),
    ('Denim Jeans', 60, 2),
    ('Winter Jacket', 120, 2),
    ('Yoga Pants', 45, 2),
    ('Organic Coffee Beans', 15, 3),
    ('Green Tea Pack', 10, 3),
    ('Dark Chocolate Bar', 5, 3),
    ('Sparkling Water 12-Pack', 12, 3),
    ('Clean Code', 40, 4),
    ('The Pragmatic Programmer', 45, 4),
    ('Design Patterns', 50, 4),
    ('Yoga Mat', 30, 5),
    ('Hiking Backpack', 85, 5),
    ('Resistance Bands Set', 20, 5);


