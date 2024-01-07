CREATE TABLE Person (
    PersonID INT AUTO_INCREMENT PRIMARY KEY,
    PersonName VARCHAR(100) NOT NULL,
    PersonTitle VARCHAR(100)
);

CREATE TABLE University (
    UniversityID INT AUTO_INCREMENT PRIMARY KEY,
    UniversityName VARCHAR(100) NOT NULL
);

CREATE TABLE Institute (
    InstituteID INT AUTO_INCREMENT PRIMARY KEY,
    InstituteName VARCHAR(100) NOT NULL,
    UniversityID INT NOT NULL,
    
    FOREIGN KEY (UniversityID)
        REFERENCES University (UniversityID)
);

CREATE TABLE Topic (
    TopicID INT AUTO_INCREMENT PRIMARY KEY,
    TopicName VARCHAR(100) NOT NULL
);

-- Main Table
CREATE TABLE Thesis (
    ThesisNo INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(500) NOT NULL,
    Abstract VARCHAR(5000) NOT NULL,
    AuthorID INT NOT NULL,
    Year INT NOT NULL,
    Type ENUM('Master', 'Doctorate', 'Specialization in Medicine', 'Proficiency in Art'),
    SupervisorID INT NOT NULL,
    CoSupervisorID INT,
    UniversityID INT NOT NULL,
    InstituteID INT NOT NULL,
    NumberOfPages INT NOT NULL,
    Language ENUM('Turkish', 'English', 'French', 'Russian', 'Arabic'),
    SubmissionDate DATE,
    Keywords varchar(100),
    
    FOREIGN KEY (AuthorID)
        REFERENCES Person (PersonID),
    FOREIGN KEY (SupervisorID)
        REFERENCES Person (PersonID),
    FOREIGN KEY (CoSupervisorID)
        REFERENCES Person (PersonID),
    FOREIGN KEY (UniversityID)
        REFERENCES University (UniversityID)
);

CREATE TABLE ThesisTopic (
    ThesisNo INT NOT NULL,
    TopicID INT NOT NULL,
    PRIMARY KEY (ThesisNo, TopicID),
    FOREIGN KEY (ThesisNo) REFERENCES Thesis (ThesisNo),
    FOREIGN KEY (TopicID) REFERENCES Topic (TopicID)
);

CREATE INDEX idx_title ON Thesis (Title);
CREATE INDEX idx_keywords ON Thesis (Keywords);
CREATE INDEX idx_author_id ON Thesis (AuthorID);
CREATE INDEX idx_topic_id ON ThesisTopic (TopicID);
CREATE INDEX idx_topic_name ON Topic (TopicName);