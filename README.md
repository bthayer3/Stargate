# Stargate System

This is a .NET 8.0 REST API project that incorporates many exciting technologies to showcase my skill sets. Some of the technologies and libraries used in this project are:
* Entity Framework Core
* SQLite Database
* In-Memory Database
* XUnit

## Breakdown

There are 2 projects. One is the "StargateAPI" that includes the following endpoints:
* Get astronaut duties by name
* Create astronaut duty
* Get all people
* Get a person by name
* Create a person
* Update a person

The other project is the "StargateTests" project which houses many integration tests that test end-to-end testing cover to edge cases and error handling to ensure the system behaves predictably even when unexpected data or errors occur.

Hoping to add a third project which would be the UI interface that interacts with the API.

## Major Changes
* Changed all SQL strings to use LINQ to prevent SQL injection
* Implemented Enum tyes for DutyTitle and Rank
* Fixed not always returning 500 error when user submits bad data, 400 error makes more sense sometimes
* Improved exception handling and added a few missing try catch statements
* Fixed multiple places where cancellation token was not being used
* Implemented asynchronous operations across all endpoints
* Removed extra project references (ex: OpenAPI and Dapper), I did not use these
* Improved general code readability such as tweaking whitespacing and condensed where possible
* Reorganized classes into appropriate folders

## How to Improve
* Reduce back-to-back select operations that are looking up similar data
* Tweak swagger UI to explain endpoints in more detail
* Consider using DateOnly instead of Datetime for models
* Prevent skipping of ranks if requirements deem this necessary
* Improve logging, consider potentially storing samples of requests and response content data
* Explore possibility of re-using code more across the endpoints or models (ex: Inheritance)
* Split Name into First and Last, ideally users would input both due to the unique check

> Note: In some places, I did leave the old code commented out for easy comparison or discussion.

## Instructions For Stargate (For Reference)

<details>
<summary>Click to expand instructions</summary>

# Stargate

***

## Astronaut Career Tracking System (ACTS)

ACTS is used as a tool to maintain a record of all the People that have served as Astronauts. When serving as an Astronaut, your *Job* (Duty) is tracked by your Rank, Title and the Start and End Dates of the Duty.

The People that exist in this system are not all Astronauts. ACTS maintains a master list of People and Duties that are updated from an external service (not controlled by ACTS). The update schedule is determined by the external service.

## Definitions

1. A person's astronaut assignment is the Astronaut Duty.
1. A person's current astronaut information is stored in the Astronaut Detail table.
1. A person's list of astronaut assignments is stored in the Astronaut Duty table.

## Requirements

##### Enhance the Stargate API (Required)

The REST API is expected to do the following:

1. Retrieve a person by name.
1. Retrieve all people.
1. Add/update a person by name.
1. Retrieve Astronaut Duty by name.
1. Add an Astronaut Duty.

##### Implement a user interface: (Encouraged)

The UI is expected to do the following:

1. Successfully implement a web application that demonstrates production level quality. Angular is preferred.
1. Implement call(s) to retrieve an individual's astronaut duties.
1. Display the progress of the process and the results in a visually sophisticated and appealing manner.

## Tasks

Overview
Examine the code, find and resolve any flaws, if any exist. Identify design patterns and follow or change them. Provide fix(es) and be prepared to describe the changes.

1. Generate the database
   * This is your source and storage location
1. Enforce the rules
1. Improve defensive coding
1. Add unit tests
   * identify the most impactful methods requiring tests
   * reach >50% code coverage
1. Implement process logging
   * Log exceptions
   * Log successes
   * Store the logs in the database

## Rules

1. A Person is uniquely identified by their Name.
1. A Person who has not had an astronaut assignment will not have Astronaut records.
1. A Person will only ever hold one current Astronaut Duty Title, Start Date, and Rank at a time.
1. A Person's Current Duty will not have a Duty End Date.
1. A Person's Previous Duty End Date is set to the day before the New Astronaut Duty Start Date when a new Astronaut Duty is received for a Person.
1. A Person is classified as 'Retired' when a Duty Title is 'RETIRED'.
1. A Person's Career End Date is one day before the Retired Duty Start Date.
</details>