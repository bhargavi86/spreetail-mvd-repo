# Spreetail.MVD

This project submission is an interactive command line app that stores a multi-value string dictionary in memory.  This application runs on the command line and can be used interactively as shown below.

## Installation

Prerequisites: Visual Studio 2022

Clone the repo and pull the latest branch. Build once, and then navigate to this path:
<RepositoryFolder>\Spreetail.MVD\bin\Debug\net6.0
Copy the contents of 'net6.0' to any desired folder, and then run the Spreetail.MVD.exe from within that new folder. Alternately, you can create a shortcut to the exe to run it from any convenient location (and eliminate the need to open the parent folder every time).

## Usage

Once you've got the app running, you can interact with it using the following commands.

1) ADD: Adds a member to a collection for a given key

```shell
> ADD
Enter key value:
foo
Enter member value:
barz
Added Member
```

2) KEYS: Returns all keys in the dictionary

```shell
> KEYS
foo;
```

3) MEMBERS: Returns collection of strings for the given key

```shell
> MEMBERS
Enter key value:
foo
barz;
```

4) REMOVE: Removes a member from a key

```shell
> REMOVE
Enter key value:
foo
Enter member value:
barz
Removed Member
```

5) ITEMS: Returns all keys in the dictionary and all of their members

```shell
> ITEMS
Key: foo Members:barz;markz;
Key: animals Members:monkey;elephant;dog;cat;
Key: sweets Members:chocolate;donuts;cake;
```

6) KEYEXISTS: Returns whether a key exists or not

```shell
> KEYEXISTS
Enter key value:
animals
True
```

7) MEMBEREXISTS: Returns whether a member exists within a key

```shell
> KEYEXISTS
Enter key value:
sweets
Enter member value:
cake
True
```
    
8) ALLMEMBERS: Returns all members in the dictionary

```shell
> ALLMEMBERS
barz;markz;monkey;elephant;dog;cat;chocolate;donuts;cake;
```

9) SORT: Sorts dictionary alphabetically by keys

```shell
> ITEMS
Key: animals Members:monkey;elephant;dog;cat;
Key: foo Members:barz;markz;
Key: sweets Members:chocolate;donuts;cake;
```

10) SORTMEMBERS: Sorts members of a key alphabetically

```shell
> KEYEXISTS
Enter key value:
animals
cat;dog;elephant;monkey;
```

11) REMOVEALL: Removes all members for a key and removes the key

```shell
> REMOVEALL
Enter key value:
foo
This Key has been removed from the dictionary

> ITEMS
Key: animals Members:monkey;elephant;dog;cat;
Key: sweets Members:chocolate;donuts;cake;
```

12) CLEAR: Removes all keys and members from the dictionary

```shell
> CLEAR

> ITEMS
There are no items in the dictionary
```

Commands are case-sensitive. In addition to the above, the ESC command exits the application, and the MENU command gives you a list of these commands with brief explanations.