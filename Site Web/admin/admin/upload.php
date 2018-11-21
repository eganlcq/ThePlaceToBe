<?php

$dir = '../image/tmp/';
    
if(isset($_FILES['fileUpload']['tmp_name']) && is_uploaded_file($_FILES['fileUpload']['tmp_name'])) {
    
    $tmp = $_FILES['fileUpload']['tmp_name'];
    $name = $_FILES['fileUpload']['name'];
    $result = move_uploaded_file($tmp, $dir . $name);
    echo json_encode($result);
}
else {
    
    echo 'FAIL';
}