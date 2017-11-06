# ThingAPI
API to Manage Things on Lorien. Used to create, update, read and delete Things. Also responsible for managing its children
## Thing Data Format
These are the fields of the thing and it's constrains:
- thingId: Id of the Thing given by the user or by de Database.
  - Integer
  - Optional on Create, mandatory on the other methods
- parentThingId: Id of the thing wich this thing belong to.
  - Integer
  - Optional
- thingName: Name of the Thing given by the user.
  - String (Up to 50 chars)
  - Mandatory
- description: Free description of the Thing.
  - String (Up to 100 chars)
  - Optional
- physicalConnection: IP address or any other value that might represent the connection between the Virutal Thing and the physical thing.
  - String (Up to 100 chars)
  - Optional
- enabled: Things cannot be deleted they are just disabled in the backend and dont show up in the queries.
  - Boolean
  - Mandatory
- thingCode: Code that might be used by the end user to identify the Thing easily.
  - String (Up to 100 chars)
  - Optional
- position: position of the Thing related to other is the same level.
  - Integer
  - Optional
- childrenThingsIds: List of Id of thing from which this one is parent.
  - Array Integer
  - Optional
### JSON Example:
```json
{
"thingId": 3,
"parentThingId": 1,
"thingName": "coisa1",
"description": "Cposa1",
"physicalConnection": null,
"enabled": true,
"thingCode": "x",
"position": 0,
"childrenThingsIds": null
}
```
## URLs
- api/things/
  - Get: Return List of Things
  - Post: Create the Thing with the JSON in the body
    - Body: Thing JSON

- api/things/{id}
  - Get: Return Thing with thingId = ID
  - Put: Update the Thing with the JSON in the body
    - Body: Thing JSON
  - Delete: Disable Thing with thingId = ID

- api/things/childrenthings/{parentId}
  - Get: Return List of Things which the parent is parentId
  - Post: Insert the Thing with the JSON in the body as child of the parent Thing
    - Body: Thing JSON
  - Delete: Remove Thing with JSON in the body as child of parent Thing.

