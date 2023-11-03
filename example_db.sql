-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Lis 02, 2023 at 12:39 PM
-- Wersja serwera: 10.4.28-MariaDB
-- Wersja PHP: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `example_db`
--

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `agents`
--

CREATE TABLE `agents` (
  `AGENT_CODE` char(6) NOT NULL,
  `AGENT_NAME` char(40) DEFAULT NULL,
  `WORKING_AREA` char(35) DEFAULT NULL,
  `COMMISSION` int(11) DEFAULT NULL,
  `PHONE_NO` char(15) DEFAULT NULL,
  `COUNTRY` varchar(25) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `agents`
--

INSERT INTO `agents` (`AGENT_CODE`, `AGENT_NAME`, `WORKING_AREA`, `COMMISSION`, `PHONE_NO`, `COUNTRY`) VALUES
('A001', 'Subbarao', 'Bangalore', 0, '077-12346674', ''),
('A002', 'Mukesh', 'Mumbai', 0, '029-12358964', ''),
('A003', 'Alex', 'London', 0, '075-12458969', ''),
('A004', 'Ivan', 'Torento', 0, '008-22544166', ''),
('A005', 'Anderson', 'Brisban', 0, '045-21447739', ''),
('A006', 'McDen', 'London', 0, '078-22255588', ''),
('A007', 'Ramasundar', 'Bangalore', 0, '077-25814763', ''),
('A008', 'Alford', 'New York', 0, '044-25874365', ''),
('A009', 'Benjamin', 'Hampshair', 0, '008-22536178', ''),
('A010', 'Santakumar', 'Chennai', 0, '007-22388644', ''),
('A011', 'Ravi Kumar', 'Bangalore', 0, '077-45625874', ''),
('A012', 'Lucida', 'San Jose', 0, '044-52981425', '');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `customer`
--

CREATE TABLE `customer` (
  `CUST_CODE` varchar(6) NOT NULL,
  `CUST_NAME` varchar(40) NOT NULL,
  `CUST_CITY` char(35) DEFAULT NULL,
  `WORKING_AREA` varchar(35) NOT NULL,
  `CUST_COUNTRY` varchar(20) NOT NULL,
  `GRADE` int(11) DEFAULT NULL,
  `OPENING_AMT` int(11) NOT NULL,
  `RECEIVE_AMT` int(11) NOT NULL,
  `PAYMENT_AMT` int(11) NOT NULL,
  `OUTSTANDING_AMT` int(11) NOT NULL,
  `PHONE_NO` varchar(17) NOT NULL,
  `AGENT_CODE` char(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `customer`
--

INSERT INTO `customer` (`CUST_CODE`, `CUST_NAME`, `CUST_CITY`, `WORKING_AREA`, `CUST_COUNTRY`, `GRADE`, `OPENING_AMT`, `RECEIVE_AMT`, `PAYMENT_AMT`, `OUTSTANDING_AMT`, `PHONE_NO`, `AGENT_CODE`) VALUES
('C00001', 'Micheal', 'New York', 'New York', 'USA', 2, 3000, 5000, 2000, 6000, 'CCCCCCC', 'A008'),
('C00002', 'Bolt', 'New York', 'New York', 'USA', 3, 5000, 7000, 9000, 3000, 'DDNRDRH', 'A008'),
('C00003', 'Martin', 'Torento', 'Torento', 'Canada', 2, 8000, 7000, 7000, 8000, 'MJYURFD', 'A004'),
('C00004', 'Winston', 'Brisban', 'Brisban', 'Australia', 1, 5000, 8000, 7000, 6000, 'AAAAAAA', 'A005'),
('C00005', 'Sasikant', 'Mumbai', 'Mumbai', 'India', 1, 7000, 11000, 7000, 11000, '147-25896312', 'A002'),
('C00006', 'Shilton', 'Torento', 'Torento', 'Canada', 1, 10000, 7000, 6000, 11000, 'DDDDDDD', 'A004'),
('C00007', 'Ramanathan', 'Chennai', 'Chennai', 'India', 1, 7000, 11000, 9000, 9000, 'GHRDWSD', 'A010'),
('C00008', 'Karolina', 'Torento', 'Torento', 'Canada', 1, 7000, 7000, 9000, 5000, 'HJKORED', 'A004'),
('C00009', 'Ramesh', 'Mumbai', 'Mumbai', 'India', 3, 8000, 7000, 3000, 12000, 'Phone No', 'A002'),
('C00010', 'Charles', 'Hampshair', 'Hampshair', 'UK', 3, 6000, 4000, 5000, 5000, 'MMMMMMM', 'A009'),
('C00011', 'Sundariya', 'Chennai', 'Chennai', 'India', 3, 7000, 11000, 7000, 11000, 'PPHGRTS', 'A010'),
('C00012', 'Steven', 'San Jose', 'San Jose', 'USA', 1, 5000, 7000, 9000, 3000, 'KRFYGJK', 'A012'),
('C00013', 'Holmes', 'London', 'London', 'UK', 2, 6000, 5000, 7000, 4000, 'BBBBBBB', 'A003'),
('C00014', 'Rangarappa', 'Bangalore', 'Bangalore', 'India', 2, 8000, 11000, 7000, 12000, 'AAAATGF', 'A001'),
('C00015', 'Stuart', 'London', 'London', 'UK', 1, 6000, 8000, 3000, 11000, 'GFSGERS', 'A003'),
('C00016', 'Venkatpati', 'Bangalore', 'Bangalore', 'India', 2, 8000, 11000, 7000, 12000, 'JRTVFDD', 'A007'),
('C00017', 'Srinivas', 'Bangalore', 'Bangalore', 'India', 2, 8000, 4000, 3000, 9000, 'AAAAAAB', 'A007'),
('C00018', 'Fleming', 'Brisban', 'Brisban', 'Australia', 2, 7000, 7000, 9000, 5000, 'NHBGVFC', 'A005'),
('C00019', 'Yearannaidu', 'Chennai', 'Chennai', 'India', 1, 8000, 7000, 7000, 8000, 'ZZZZBFV', 'A010'),
('C00020', 'Albert', 'New York', 'New York', 'USA', 3, 5000, 7000, 6000, 6000, 'BBBBSBB', 'A008'),
('C00021', 'Jacks', 'Brisban', 'Brisban', 'Australia', 1, 7000, 7000, 7000, 7000, 'WERTGDF', 'A005'),
('C00022', 'Avinash', 'Mumbai', 'Mumbai', 'India', 2, 7000, 11000, 9000, 9000, '113-12345678', 'A002'),
('C00023', 'Karl', 'London', 'London', 'UK', 0, 4000, 6000, 7000, 3000, 'AAAABAA', 'A006'),
('C00024', 'Cook', 'London', 'London', 'UK', 2, 4000, 9000, 7000, 6000, 'FSDDSDF', 'A006'),
('C00025', 'Ravindran', 'Bangalore', 'Bangalore', 'India', 2, 5000, 7000, 4000, 8000, 'AVAVAVA', 'A011');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `orders`
--

CREATE TABLE `orders` (
  `ORD_NUM` int(11) NOT NULL,
  `ORD_AMOUNT` int(11) NOT NULL,
  `ADVANCE_AMOUNT` int(11) NOT NULL,
  `ORD_DATE` date NOT NULL,
  `CUST_CODE` varchar(6) NOT NULL,
  `AGENT_CODE` char(6) NOT NULL,
  `ORD_DESCRIPTION` varchar(60) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `orders`
--

INSERT INTO `orders` (`ORD_NUM`, `ORD_AMOUNT`, `ADVANCE_AMOUNT`, `ORD_DATE`, `CUST_CODE`, `AGENT_CODE`, `ORD_DESCRIPTION`) VALUES
(200100, 1000, 600, '0000-00-00', 'C00013', 'A003', 'SOD'),
(200101, 3000, 1000, '0000-00-00', 'C00001', 'A008', 'SOD'),
(200102, 2000, 300, '0000-00-00', 'C00012', 'A012', 'SOD'),
(200103, 1500, 700, '0000-00-00', 'C00021', 'A005', 'SOD'),
(200104, 1500, 500, '0000-00-00', 'C00006', 'A004', 'SOD'),
(200105, 2500, 500, '0000-00-00', 'C00025', 'A011', 'SOD'),
(200106, 2500, 700, '0000-00-00', 'C00005', 'A002', 'SOD'),
(200107, 4500, 900, '0000-00-00', 'C00007', 'A010', 'SOD'),
(200108, 4000, 600, '0000-00-00', 'C00008', 'A004', 'SOD'),
(200109, 3500, 800, '0000-00-00', 'C00011', 'A010', 'SOD'),
(200110, 3000, 500, '0000-00-00', 'C00019', 'A010', 'SOD'),
(200111, 1000, 300, '0000-00-00', 'C00020', 'A008', 'SOD'),
(200112, 2000, 400, '0000-00-00', 'C00016', 'A007', 'SOD'),
(200113, 4000, 600, '0000-00-00', 'C00022', 'A002', 'SOD'),
(200114, 3500, 2000, '0000-00-00', 'C00002', 'A008', 'SOD'),
(200116, 500, 100, '0000-00-00', 'C00010', 'A009', 'SOD'),
(200117, 800, 200, '0000-00-00', 'C00014', 'A001', 'SOD'),
(200118, 500, 100, '0000-00-00', 'C00023', 'A006', 'SOD'),
(200119, 4000, 700, '0000-00-00', 'C00007', 'A010', 'SOD'),
(200120, 500, 100, '0000-00-00', 'C00009', 'A002', 'SOD'),
(200121, 1500, 600, '0000-00-00', 'C00008', 'A004', 'SOD'),
(200122, 2500, 400, '0000-00-00', 'C00003', 'A004', 'SOD'),
(200123, 500, 100, '0000-00-00', 'C00022', 'A002', 'SOD'),
(200124, 500, 100, '0000-00-00', 'C00017', 'A007', 'SOD'),
(200125, 2000, 600, '0000-00-00', 'C00018', 'A005', 'SOD'),
(200126, 500, 100, '0000-00-00', 'C00022', 'A002', 'SOD'),
(200127, 2500, 400, '0000-00-00', 'C00015', 'A003', 'SOD'),
(200128, 3500, 1500, '0000-00-00', 'C00009', 'A002', 'SOD'),
(200129, 2500, 500, '0000-00-00', 'C00024', 'A006', 'SOD'),
(200130, 2500, 400, '0000-00-00', 'C00025', 'A011', 'SOD'),
(200131, 900, 150, '0000-00-00', 'C00012', 'A012', 'SOD'),
(200133, 1200, 400, '0000-00-00', 'C00009', 'A002', 'SOD'),
(200134, 4200, 1800, '0000-00-00', 'C00004', 'A005', 'SOD'),
(200135, 2000, 800, '0000-00-00', 'C00007', 'A010', 'SOD');

--
-- Indeksy dla zrzutów tabel
--

--
-- Indeksy dla tabeli `agents`
--
ALTER TABLE `agents`
  ADD PRIMARY KEY (`AGENT_CODE`);

--
-- Indeksy dla tabeli `customer`
--
ALTER TABLE `customer`
  ADD PRIMARY KEY (`CUST_CODE`);

--
-- Indeksy dla tabeli `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`ORD_NUM`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
