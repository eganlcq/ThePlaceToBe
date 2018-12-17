<?php

$image = '';

if(!empty($_POST)) {
        $image = ' /var/www/html/admin/textRecognition/images/' . $_POST['image'];
        
}

if($image != '') {
    
    $python='/usr/local/bin/python3.6';
    $script=' /var/www/html/admin/textRecognition/text_recognition.py';
    $arg1=' --east /var/www/html/admin/textRecognition/frozen_east_text_detection.pb';
    $arg2=' --image';
    $arg3=' -w 32';
    $arg4=' -e 32';
    $tab = array();
    exec($python . $script . $arg1 . $arg2 . $image . $arg3 . $arg4, $tab);
    echo json_encode($tab);
}
else {
    
    echo 'ERROR';
}

