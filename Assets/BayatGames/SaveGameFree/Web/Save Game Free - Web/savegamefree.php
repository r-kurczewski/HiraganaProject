<?php

/**
 * Welcome to Save Game Free Web
 *
 * We save the sended data using HTTP POST method.
 * All data is stored in $_POST['data'];
 *
 * There is some information you must enter.
 */

// Enter the Database Host, Most of the time is "localhost"
define('DB_HOST', 'localhost');

// Enter the Database User name
define('DB_USER', 'root');

// Enter the Database User password
define('DB_PASS', 'root');

// Enter the Database name used for saves
define('DB_NAME', 'savegamefree');

// Enter the database table
define('DB_TABLE', 'savegames');

// Enter the username that you have used in unity
define('USERNAME', 'savegamefree');

// Enter the password that you have used in unity
define('PASSWORD', '$@ve#game%free');

/**
 * All done.
 *
 * You don't need to edit any of the below.
 */

header("Cache-Control: no-cache, no-store, must-revalidate");
header("Pragma: no-cache");
header("Expires: 0");
header("Content-Type", "text/plain");

if (!isset($_POST)) {
    echo "Error: Only HTTP POST method supported.";
    exit;
}

if ($_POST['username'] !== USERNAME || $_POST['password'] !== PASSWORD) {
    echo "Error: Failed, authentication credentials are incorrect.\n";
    echo "Username: " . ($_POST['username'] === USERNAME ? "Valid" : "Invalid") . "\n";
    echo "Password: " . ($_POST['password'] === PASSWORD ? "Valid" : "Invalid") . "\n";
    exit;
}

$mysqli = new mysqli(DB_HOST, DB_USER, DB_PASS, DB_NAME);

if ($mysqli->connect_errno) {
    echo "Error: Failed to connect to MySQL: " . $mysqli->connect_error;
    exit;
}

switch ($_POST['action']) {
  case 'save':
    $query = "INSERT INTO `" . DB_TABLE . "` (`id`, `data`) VALUES (?, ?) ON DUPLICATE KEY UPDATE data=VALUES (data)";
    $stmt = $mysqli->prepare($query);
    if ($stmt) {
        $stmt->bind_param('ss', $_POST['identifier'], $_POST['data']);
        $stmt->execute();
        $stmt->close();
        echo "Success";
    } else {
        echo "Error: Failed to query MySQL: " . $mysqli->error;
    }
    break;

  case 'load':
    $query = "SELECT data FROM `" . DB_TABLE . "` WHERE id=?";
    $stmt = $mysqli->prepare($query);
    if ($stmt) {
        $stmt->bind_param('s', $_POST['identifier']);
        $stmt->execute();
        $stmt->store_result();
        $stmt->bind_result($data);
        while ($stmt->fetch()) {
            echo $data;
        }
        $stmt->close();
    } else {
        echo "Error: Failed to query MySQL: " . $mysqli->error;
    }
    break;

  case 'getall':
    $query = "SELECT id FROM `" . DB_TABLE . "`";
    $stmt = $mysqli->prepare($query);
    if ($stmt) {
        $stmt->bind_param('s', $_POST['identifier']);
        $stmt->execute();
        $stmt->store_result();
        $stmt->bind_result($data);
        while ($stmt->fetch()) {
            echo $data;
            echo "\n";
        }
        $stmt->close();
    } else {
        echo "Error: Failed to query MySQL: " . $mysqli->error;
    }
    break;

  case 'exists':
    $query = "SELECT 1 FROM `" . DB_TABLE . "` WHERE id=?";
    $stmt = $mysqli->prepare($query);
    if ($stmt) {
        $stmt->bind_param('s', $_POST['identifier']);
        $stmt->execute();
        $stmt->store_result();
        $stmt->bind_result($data);
        $stmt->fetch();
        if ($data) {
            echo "true";
        } else {
            echo "false";
        }
        $stmt->close();
    } else {
        echo "Error: Failed to query MySQL: " . $mysqli->error;
    }
    break;

  default:
    echo 'Error: The given action does not exists, please specify a valid action. (save or load)';
    break;
}

$mysqli->close();
exit;
;
