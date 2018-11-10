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
            $this->iPdo = new PDO('mysql:host=172.17.0.3;port=3306;dbname=theplacetobe;charset=utf8', 'admin', 'jej111jej222');
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

    public function call($nom, $param= []){
        $p =[];
        switch ($nom){
            case 'gestioncompte':
            case 'userinscription':array_push($p, '?', '?', '?');
            case 'verifpseudomail': array_push($p, '?');
            case 'verifmdp':
            case 'userconnexion':array_push($p, '?');
            case 'achievementbyuser':
            case 'favorisbyuser':
            case 'verifpseudo':
            case 'verifmail': array_push($p, '?');
                try {
                    $appel = 'call '.$nom.'('.implode(',', $p).')';
                    $sth = $this->iPdo->prepare($appel);
                    $sth->execute($param);
                    return $sth->fetchAll(PDO::FETCH_ASSOC);
                }
                catch(PDOException $e){
                    $this->pdoException = $e;
                    return ['__ERR__'=>$this->getException()];
                }
                break;
            default : return ['__ERR__' => 'call impossible à '.$nom];
        }
    }
}

