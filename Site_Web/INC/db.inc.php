<?php

if(count(get_included_files())==1) die('--access denied--');

class Db
{
    private $db = [];
    private $pdoException = null;
    private $iPdo = null;

    public function __construct()
    {
        try {
            $this->iPdo = new PDO('mysql:host=172.17.0.3;port=3306;dbname=theplacetobe', 'admin', 'jej111jej222');
            $this->iPdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
        }
        catch (PDOException $e){
            $this->pdoException = $e;
        }
    }

    public function getIPdo()
    {
        return $this->iPdo;
    }

    public function getException(){
        return 'PDOException : '.($this->pdoException ? $this->pdoException->getMessage() : 'aucune !');
    }

}

