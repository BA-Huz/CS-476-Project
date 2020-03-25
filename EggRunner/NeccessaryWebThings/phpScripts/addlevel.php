<?php 
	$db = new mysqli("localhost", "fries20v", "Tower31", "fries20v");
	if ($db->connect_error)
	{
	    	echo "Connection Failed";
	    	die ("Connection failed: " . $db->connect_error);
	}


	$author = $_POST["author"];
	//Use default if no name supplised
	if ($author === ''){
		$author = "Anonymous";
	}
        $levelstring = $_POST["levelstring"]; 
         
        //Insert into LEVELS
	$stmt = $db->prepare("INSERT INTO LEVELS VALUES (NULL, ?, ?)");
	$stmt->bind_param("ss", $author, $levelstring);
	$stmt->execute();
	
	$stmt->close();
	$db->close();
?>