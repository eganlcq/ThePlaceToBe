<?php

$dir = 'textRecognition/images/';
    
if(isset($_FILES['fileUpload']['tmp_name']) && is_uploaded_file($_FILES['fileUpload']['tmp_name'])) {
    
    $tmp = $_FILES['fileUpload']['tmp_name'];
    $name = $_FILES['fileUpload']['name'];
    $result = move_uploaded_file($tmp, $dir . $name);
    if($result == false) {
        
        echo json_encode($_FILES['fileUpload']);
    }
    else echo json_encode($result);
}
else {
    
    echo 'FAIL';
}