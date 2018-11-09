<?php

    class Database {
        
        private static $dbHost = "51.38.239.219";
        private static $dbName = "theplacetobe";
        private static $dbUser = "admin";
        private static $dbPswd = "jej111jej222";
        
        private static $connection = null;

        public static function connect() {
            try {
                self::$connection = new PDO('mysql:host=' . self::$dbHost . ';port=3306;dbname=' . self::$dbName, self::$dbUser, self::$dbPswd);
                self::$connection->exec('SET NAMES utf8');
            }
            catch(Exception $e) {
                die($e->getMessage());
            }
            return self::$connection;
        }
        
        public static function disconnect() {
            self::$connection = null;
        }
        
    }
