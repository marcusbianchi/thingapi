# ThingAPI

API to Manage Things on Lorien. Used to create, update, read and delete Things.
Also responsible for managing its children. Default Port: 5001

## Thing Data Format

These are the fields of the thing and it's constrains:

* thingId: Id of the Thing given by de Database.
  * Integer
  * Ignored on Create, mandatory on the other methods
* parentThingId: Id of the thing wich this thing belong to.
  * Integer
  * Ignored on Create and Update
* thingName: Name of the Thing given by the user.
  * String (Up to 50 chars)
  * Mandatory
* description: Free description of the Thing.
  * String (Up to 100 chars)
  * Optional
* physicalConnection: IP address or any other value that might represent the
  connection between the Virutal Thing and the physical thing.
  * String (Up to 100 chars)
  * Optional
* enabled: Things cannot be deleted they are just disabled in the backend and
  dont show up in the queries.
  * Boolean
  * Mandatory
* thingCode: Code that might be used by the end user to identify the Thing
  easily.
  * String (Up to 100 chars)
  * Optional
* position: position of the Thing related to other is the same level.
  * Integer
  * Optional
* childrenThingsIds: List of Id of thing from which this one is parent.

  * Array Integer
  * Ignored on Create and Update

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

* api/things/{optional=startat}{optional=quantity}

  * Get: Return List of Things
    * startat: represent where the list starts t the database (Default=0)
    * quantity: number of resuls in the query (Default=50)
  * Post: Create the Thing with the JSON in the body
    * Body: Thing JSON

* api/things/{id}

  * Get: Return Thing with thingId = ID
  * Put: Update the Thing with the JSON in the body with thingId = ID
    * Body: Thing JSON
  * Delete: Disable Thing with thingId = ID

* api/things/list{thingid}{thingid}

  * Get: Return List of Things with thingId = ID

* api/things/childrenthings/{parentId}
  * Get: Return List of Things which the parent is parentId
  * Post: Insert the Thing with the JSON in the body as child of the parent
    Thing
    * Body: Thing JSON
  * Delete: Remove Thing with JSON in the body as child of parent Thing.
    * Body: Thing JSON

# ThingGroupAPI

API to Manage Groups of Things on Lorien. Used to create, update, read and
delete groups. Also responsible for managing its members.

## Thing Group Data Format

These are the fields of the thing and it's constrains:

* thingGroupId: Id of the Group given by de Database.
  * Integer
  * Ignored on Create, mandatory on the other methods
* groupName: Name of the Group given by the user.
  * String (Up to 50 chars)
  * Mandatory
* groupDescription: Free description of the Group.
  * String (Up to 100 chars)
  * Optional
* groupPrefix:Prefix that identifies Tags of this group.
  * String (Up to 50 chars)
  * Optional
* enabled: Groups cannot be deleted they are just disabled in the backend and
  dont show up in the queries.
  * Boolean
  * Mandatory
* groupCode: Code that might be used by the end user to identify the Group
  easily.
  * String (Up to 100 chars)
  * Optional
* thingsIds: List of Id of things that belong to this group.
  * Array Integer
  * Ignored on Create and Update
* parameters: List of the group's parameters
  * Array Parameters
  * Ignored on Create and Update
    ### JSON Ex ample:

```json
{
  "thingGroupId": 2,
  "groupName": "teste52",
  "groupDescription": "teste",
  "enabled": true,
  "groupCode": "teste",
  "groupPrefix": "teste_prefix",
  "thingsIds": [],
  "tags": [
    {
      "tagId": 1,
      "tagName": "da",
      "tagDescription": "das",
      "physicalTag": "asda"
    }
  ]
}
```

## URLs

* api/thinggroups/{optional=startat}{optional=quantity}

  * Get: Return List of Groups
    * startat: represent where the list starts t the database (Default=0)
    * quantity: number of resuls in the query (Default=50)
  * Post: Create the Group with the JSON in the body
    * Body: Group JSON

* api/thinggroups/getgroups/{thingid}

  * Get: Return List of Groups that contains that thingId.

* api/thinggroups/{id}

  * Get: Return Group with GroupId = ID
  * Put: Update the Group with the JSON in the body with GroupId = ID
    * Body: Thing JSON
  * Delete: Disable Group with thingId = ID

* api/thinggroups/attachedthings/{groupId}
  * Get: Return List of Things in the group where GroupId = groupId
  * Post: Insert the Thing with the JSON in the body in the group where GroupId
    = groupId
    * Body: Thing JSON
  * Delete: Remove Thing with JSON in the body of the group where GroupId =
    groupId

    * Body: Thing JSON
* api/thinggroups/list{thingGroupId}{thingGroupId}

  * Get: Return List of Things Groups with thingGroupId = ID

# TagAPI

API to manage Tags Groups of Things on Lorien. Used to create, update, read and
delete tags.

## Tag Data Format

These are the fields of the tag and it's constrains:

* tagId: Id of the Tag given by de Database.
  * Integer
  * Ignored on Create, mandatory on the other methods
* tagName: Name of the Tag given by the user.
  * String (Up to 50 chars)
  * Mandatory
* tagDescription: Free description of the Tag.
  * String (Up to 100 chars)
  * Optional
* physicalTag: Name of the Tag that represent this parameter on the real world.
  * String (Up to 100 chars)
  * Optional
* tagGroup: Name of group that the tag belongs to.
  * String (Up to 50 chars)
  * Optional
* tagType: type of tag Input or Output.
  * Enum (Input or Output)
  * Required
* thingGroupId: Id of group that has this parameter.
  * Integer
  * Mandatory on Create and on Update
* thingGroup: Object of the group which this parameter belongs to:

  * ThingGroup JSON
  * Ignored on Create, mandatory on the other methods

  ### JSON Example:

```json
{
  "tagId": 2,
  "tagDescription": "trus",
  "tagName": "da",
  "physicalTag": "asda",
  "tagGroup": "teste",
  "tagType": "Output",
  "thingGroupId": 1,
  "thingGroup": {
    "thingGroupId": 1,
    "groupCode": "teste",
    "groupDescription": "teste",
    "groupName": "teste",
    "thingsIds": []
  }
}
```

## URLs

* api/tags/{optional=startat}{optional=quantity}{optional=orderField}{optional=order}{optional=fieldFilter}{optional=fieldValue}

  * Get: Return List of Tags
    * startat: represent where the list starts t the database (Default=0)
    * quantity: number of resuls in the query (Default=50)
    * orderField: Field in which the list will be order by (Possible Values:
      productName,productDescription, productCode,
      productGTIN)(Default=ProductId)
    * order: Represent the order of the listing (Possible Values: ascending,
      descending)(Default=Ascending)
    * fieldFilter: represents the field that will be seached (Possible Values:
      productName,productDescription, productCode, productGTIN) (Default=null)
    * fieldValue: represents de valued searched on the field (Default=null)
  * Post: Create the Tag with the JSON in the body
    * Body: Tag JSON

* api/tags/{id}

  * Get: Return Tag with tagId = ID
  * Put: Update the Tag with the JSON in the body with tagId = ID
    * Body: Tag JSON
  * Delete: Delete the Tag from the Database with tagId = ID

* api/tags/list{tagid}{tagid}
  * Get: Return List of Tags with tagId = ID
