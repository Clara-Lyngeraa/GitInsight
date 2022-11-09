# Requirements

## Functional requirements

* The program must be able to receive a user-specified path to a Git repository that resides in a local directory
* The program must collect all commits with respective author names and author dates
* The program must be able to run in two switchable modes
* Commit frequency mode must produce textual output on stdout that lists the number of commits per day
* Commit author mode must produce textual output on stdout that lists the number of commits per day per author
* The program must store the most recent commit at the time of the analysis of each analyzed Git repository
* The program must update the stored data to the most current analysis results if a Git repository is re-analyzed
* The program must skip re-analyzing a Git repository if the analysis results that correspond to the most current state of the repository and the output should be generated from the readily available data
* The program (REST API?)  must clone a remote repository from GitHub and store it in a temporary local directory if it does not exist locally
* The program must update a already cloned repository

## Non-functional requirements

* The program must be a C#/.Net Core application
* The program must be developed in a test-driven manner
* The program must have one or multiple GitHub Action workflows that manages every pull and push to the main branch
* The program must store the analyzed Git repositories and their states in a database
* The program must be a web-application that exposes a REST API
* The program (REST API?) must receive a repository identifier from GitHub in the form <github_user>/<repository_name> or of the form <github_organization>/<repository_name>
* The program (REST API?) must return the analysis results via a JSON objects
