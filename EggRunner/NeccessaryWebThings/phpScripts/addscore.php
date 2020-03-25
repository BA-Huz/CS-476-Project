<?php 
	$db = new mysqli("localhost", "fries20v", "Tower31", "fries20v");
	if ($db->connect_error)
	{
	    	echo "Connection Failed";
	    	die ("Connection failed: " . $db->connect_error);
	}


	$player = $_POST["player"];
	$level = (int)$_POST["level"];
	$score = (int)$_POST["score"];

	//Use default if no name supplised
	if ($author === ''){
		$author = "Anonymous";
	}
         
        //Insert into LEVELS
	$stmt = $db->prepare("INSERT INTO HIGHSCORES VALUES (NULL, ?, ?, ?)");
	$stmt->bind_param("isi", $level, $player, $score);
	$stmt->execute();
	
	$stmt->close();
	$db->close();
?>