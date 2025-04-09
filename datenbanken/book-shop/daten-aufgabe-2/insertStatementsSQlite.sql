-- Beispiel-Daten für die Tabelle customers
INSERT INTO customers (first_name, last_name, email, phone) VALUES
('Max', 'Mustermann', 'max@example.com', '123456789'),
('Erika', 'Musterfrau', 'erika@example.com', '987654321'),
('John', 'Doe', 'john.doe@example.com', '111222333'),
('Alice', 'Wonderland', 'alice@example.com', '444555666'),
('Bob', 'Builder', 'bob@example.com', '777888999'),
('Charlie', 'Brown', 'charlie@example.com', '111333555'),
('David', 'Smith', 'david@example.com', '222444666'),
('Eva', 'Johnson', 'eva@example.com', '333555777'),
('Frank', 'Miller', 'frank@example.com', '444666888'),
('Grace', 'Davis', 'grace@example.com', '555777999'),
('Hannah', 'White', 'hannah@example.com', '666888000'),
('Isaac', 'Taylor', 'isaac@example.com', '777999111'),
('Jack', 'Anderson', 'jack@example.com', '888000222'),
('Karen', 'Moore', 'karen@example.com', '999111333'),
('Liam', 'Wilson', 'liam@example.com', '000222444');

-- Beispiel-Daten für die Tabelle states
INSERT INTO states (state) VALUES
('Pending'),
('Shipped'),
('Delivered'),
('Cancelled');

-- Beispiel-Daten für die Tabelle publishers
INSERT INTO publishers (name, city, website) VALUES
('Penguin Books', 'London', 'https://www.penguin.co.uk'),
('HarperCollins', 'New York', 'https://www.harpercollins.com'),
('Random House', 'Berlin', 'https://www.randomhouse.de'),
('Macmillan', 'London', 'https://www.macmillan.com'),
('Simon & Schuster', 'New York', 'https://www.simonandschuster.com');

-- Beispiel-Daten für die Tabelle autors
INSERT INTO autors (first_name, last_name, year_of_birth) VALUES
('Stephen', 'King', 1947),
('J.K.', 'Rowling', 1965),
('George', 'Orwell', 1903),
('Jane', 'Austen', 1775),
('Mark', 'Twain', 1835);

-- Beispiel-Daten für die Tabelle books
INSERT INTO books (isbn, title, price, autor_id, publisher_id) VALUES
('978-3-16-148410-0', 'The Shining', 19.99, 1, 1),
('978-0-7432-7356-5', 'Harry Potter and the Sorcerer''s Stone', 15.99, 2, 2),
('978-0-452-28423-4', '1984', 12.99, 3, 3),
('978-0-19-283355-4', 'Pride and Prejudice', 9.99, 4, 1),
('978-0-14-143956-3', 'Adventures of Huckleberry Finn', 14.99, 5, 4),
('978-1-56619-909-4', 'Moby Dick', 18.99, 5, 1),
('978-0-307-27778-6', 'The Great Gatsby', 14.50, 4, 2),
('978-0-670-03255-7', 'To Kill a Mockingbird', 16.99, 3, 3),
('978-0-679-74532-7', 'Brave New World', 13.99, 2, 4),
('978-0-141-18790-4', 'Wuthering Heights', 12.50, 1, 5),
('978-0-141-18790-7', 'Wuthering Heights II', 14.50, 1, 5);

-- Beispiel-Daten für die Tabelle warehouses
INSERT INTO warehouses (name) VALUES
('Main Warehouse'),
('East Warehouse'),
('West Warehouse');

-- Beispiel-Daten für die Tabelle warehouse_books
INSERT INTO warehouse_books (warehouse_id, book_id, book_count) VALUES
(1, 1, 100),
(1, 2, 23),
(1, 3, 50),
(1, 4, 76),
(1, 5, 15),
(1, 6, 1),
(1, 7, 4),
(1, 8, 6),
(1, 9, 50),
(1, 10, 30),
(2, 3, 200),
(2, 4, 5),
(2, 5, 40),
(2, 6, 67),
(2, 7, 44),
(2, 8, 67),
(2, 9, 42),
(3, 1, 75),
(3, 3, 7),
(3, 5, 45),
(3, 7, 74),
(3, 9, 15),
(3, 10, 45);

-- Beispiel-Daten für die Tabelle orders
INSERT INTO orders (customer_id, state_id, order_date) VALUES
(1, 1, '2024-02-01 10:15:00'),
(2, 2, '2024-02-02 14:30:00'),
(3, 3, '2024-02-03 09:45:00'),
(4, 1, '2024-02-04 16:20:00'),
(5, 2, '2024-02-05 11:10:00'),
(6, 1, '2024-02-06 18:35:00'),
(7, 2, '2024-02-07 13:00:00'),
(8, 3, '2024-02-08 07:25:00'),
(9, 1, '2024-02-09 15:45:00'),
(10, 2, '2024-02-10 12:55:00'),
(11, 3, '2024-02-11 14:05:00'),
(12, 1, '2024-02-12 10:50:00'),
(13, 2, '2024-02-13 17:15:00'),
(14, 3, '2024-02-14 19:30:00'),
(15, 1, '2024-02-15 08:40:00'),
(6, 2, '2024-02-16 09:55:00'),
(7, 3, '2024-02-17 20:10:00'),
(8, 1, '2024-02-18 22:25:00'),
(9, 2, '2024-02-19 11:35:00'),
(10, 3, '2024-02-20 16:50:00');

-- Beispiel-Daten für die Tabelle book_orders
INSERT INTO book_orders (book_id, order_id, amount) VALUES
(1, 1, 1),
(2, 1, 2),
(3, 1, 1),
(3, 3, 2),
(4, 4, 3),
(2, 4, 1),
(5, 5, 1),
(6, 6, 1),
(7, 7, 1),
(8, 8, 1),
(9, 9, 2),
(10, 9, 2),
(10, 10, 1),
(1, 11, 2),
(10, 8, 1),
(2, 12, 2),
(3, 13, 1),
(4, 14, 1),
(5, 15, 1),
(6, 16, 1),
(7, 17, 1),
(8, 18, 1),
(9, 19, 1),
(10, 20, 1);