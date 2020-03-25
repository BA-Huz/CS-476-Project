<?php 
	$db = new mysqli("localhost", "fries20v", "Tower31", "fries20v");
	if ($db->connect_error)
	{
	    	echo "Connection Failed";
	    	die ("Connection failed: " . $db->connect_error);
	}


	$id = (int)$_POST["id"];
	//echo $id; 
        //Get Level String 
	$stmt = $db->prepare("SELECT LEVELSTRING FROM LEVELS WHERE ID=?");
	$stmt->bind_param("i", $id);
	$stmt->execute();
	$stmt->bind_result($col1);
	$stmt->fetch();
	echo $col1;

	
	$stmt->close();
	$db->close();
?>