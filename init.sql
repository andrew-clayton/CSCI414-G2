CREATE DATABASE book_exchange;
\c book_exchange; -- connect to this db

-- Table for Schools
CREATE TABLE Schools (
    SchoolId SERIAL PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    State CHAR(2) NOT NULL
);

-- Table for Students
CREATE TABLE Students (
    StudentId SERIAL PRIMARY KEY,
    FirstName VARCHAR(20) NOT NULL,
    LastName VARCHAR(30) NOT NULL,
    SchoolId INT,
    FieldOfStudy VARCHAR(40) NOT NULL,
    FOREIGN KEY (SchoolId) REFERENCES Schools(SchoolId)
);

-- Table for Books
CREATE TABLE Books (
    BookId SERIAL PRIMARY KEY,
    Title VARCHAR(50) NOT NULL,
    Description VARCHAR(500) NOT NULL,
    Author VARCHAR(200) -- Nullable
);

-- Table for Courses
CREATE TABLE Courses (
    CourseId SERIAL PRIMARY KEY,
    CourseName VARCHAR(30) NOT NULL,
    CourseNumber INT NOT NULL,
    Professor VARCHAR(150), -- Nullable
    FieldOfStudy VARCHAR(40) NOT NULL,
    SchoolId INT NOT NULL,
    BookId INT,
    FOREIGN KEY (SchoolId) REFERENCES Schools(SchoolId),
    FOREIGN KEY (BookId) REFERENCES Books(BookId)
);

-- Table for BookOfferings
CREATE TABLE BookOffering (
    BookOfferingId SERIAL PRIMARY KEY,
    BookId INT NOT NULL,
    AvailabilityStatus VARCHAR(20) NOT NULL, -- e.g., 'Available', 'Unavailable'
    Price DECIMAL(10, 2) NOT NULL,
    StudentId INT NOT NULL,
    FOREIGN KEY (BookId) REFERENCES Books(BookId),
    FOREIGN KEY (StudentId) REFERENCES Students(StudentId)
);

-- Table for SavedBooks
CREATE TABLE SavedBooks (
    SavedBookId SERIAL PRIMARY KEY,
    BookId INT NOT NULL,
    StudentId INT NOT NULL,
    FOREIGN KEY (BookId) REFERENCES Books(BookId),
    FOREIGN KEY (StudentId) REFERENCES Students(StudentId)
);

-- Table for Notifications
CREATE TABLE Notifications (
    NotificationId SERIAL PRIMARY KEY,
    StudentId INT NOT NULL,
    BookOfferingId INT NOT NULL,
    NotificationType VARCHAR(20) NOT NULL, -- e.g., 'New Offer', 'Price Change'
    Timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    FOREIGN KEY (StudentId) REFERENCES Students(StudentId),
    FOREIGN KEY (BookOfferingId) REFERENCES BookOffering(BookOfferingId)
);
