# Pharmacy Management System - Deployment Guide

## System Requirements
- OS: Windows 10 / 11
- .NET Framework 4.7.2 or higher
- MySQL Server 8.0 or higher
- Visual Studio 2019/2022

## Step 1 - MySQL Install karo
Download karo: https://dev.mysql.com/downloads/mysql/
Install karo aur root password set karo

## Step 2 - Database Setup karo
CMD mein yeh likho:

mysql -u root -p

Password enter karo, phir:

CREATE DATABASE pharmacy;
USE pharmacy;
exit

Database import karo:

mysql -u root -p pharmacy < database.sql

## Step 3 - Project Setup karo
- ZIP extract karo
- Visual Studio mein WindowsFormsApp1.sln open karo
- App.config mein connection string update karo:
  server=localhost;user=root;password=YOUR_PASSWORD;database=pharmacy;
- YOUR_PASSWORD ki jagah apna MySQL password likho

## Step 4 - Run karo
- Visual Studio mein F5 dabao

## Default Login Credentials
Admin Username: admin
Admin Password: admin1122
User Username: user
User Password: user123

## Git Repository
https://github.com/alirazaijaz139/PharmacyManagement