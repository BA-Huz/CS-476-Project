<?php 
	$db = new mysqli("localhost", "fries20v", "Tower31", "fries20v");
	if ($db->connect_error)
	{
	    	echo "Connection Failed";
	    	die ("Connection failed: " . $db->connect_error);
	}

	$count = (int)$_POST["count"];
	$offset = (int)$_POST["offset"];		

        //Get Level String 
	$stmt = $db->prepare("SELECT ID, AUTHOR FROM LEVELS ORDER BY ID LIMIT ?, ?");
	$stmt->bind_param("ii", $offset, $count);
	$stmt->execute();
	$stmt->bind_result($id, $author);
	
	$returnString = "";

	while($stmt->fetch()){
		$returnString .= $id .','. $author .','. '-,-,';
	}
	$stmt->close();	

	$returnString = rtrim($returnString, ",");
	echo $returnString;

	
	$db->close();
?>