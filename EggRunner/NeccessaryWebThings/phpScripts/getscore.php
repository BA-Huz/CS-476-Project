<?php 
	$db = new mysqli("localhost", "fries20v", "Tower31", "fries20v");
	if ($db->connect_error)
	{
	    	echo "Connection Failed";
	    	die ("Connection failed: " . $db->connect_error);
	}


	$id = (int)$_POST["id"];
        //Get Score
	$stmt = $db->prepare("SELECT USER, SCORE FROM HIGHSCORES WHERE LEVEL=? ORDER BY SCORE DESC LIMIT 1");
	$stmt->bind_param("i", $id);
	$stmt->execute();
	$stmt->bind_result($user, $score);
	
	$returnString = "";

	while($stmt->fetch()){
		$returnString .= $user .','. $score;
	}
	$stmt->close();	

	echo $returnString;
	//echo "test";

	$db->close();
?>