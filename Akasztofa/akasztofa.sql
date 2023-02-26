-- phpMyAdmin SQL Dump
-- version 4.9.7
-- https://www.phpmyadmin.net/
--
-- Gép: localhost
-- Létrehozás ideje: 2023. Feb 15. 13:55
-- Kiszolgáló verziója: 10.3.32-MariaDB
-- PHP verzió: 7.4.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `akasztofa`
--
CREATE DATABASE IF NOT EXISTS `akasztofa` DEFAULT CHARACTER SET utf8 COLLATE utf8_hungarian_ci;
USE `akasztofa`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `felhasznalok`
--

CREATE TABLE `felhasznalok` (
  `ui` varchar(50) COLLATE utf8_hungarian_ci NOT NULL,
  `pw` varchar(50) COLLATE utf8_hungarian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `jatekok`
--

CREATE TABLE `jatekok` (
  `id` int(11) NOT NULL,
  `fid` varchar(50) COLLATE utf8_hungarian_ci NOT NULL,
  `konnyuossz` int(5) NOT NULL DEFAULT 0,
  `konnyunyert` int(5) NOT NULL DEFAULT 0,
  `kozepesossz` int(5) NOT NULL DEFAULT 0,
  `kozepesnyert` int(5) NOT NULL DEFAULT 0,
  `nehezossz` int(5) NOT NULL DEFAULT 0,
  `neheznyert` int(5) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `szavak`
--

CREATE TABLE `szavak` (
  `szo` varchar(30) COLLATE utf8_hungarian_ci NOT NULL,
  `nehezseg` int(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `felhasznalok`
--
ALTER TABLE `felhasznalok`
  ADD PRIMARY KEY (`ui`);

--
-- A tábla indexei `jatekok`
--
ALTER TABLE `jatekok`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `szavak`
--
ALTER TABLE `szavak`
  ADD PRIMARY KEY (`szo`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `jatekok`
--
ALTER TABLE `jatekok`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
