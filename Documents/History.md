# History #

**v1.1.2**

- Modified the keyboard shortcuts to:

    CTRL-ALT-A (Show Acknowledgement window)
    CTRL-ALT-S (Show Signature window)

**v1.1.1**

- Added Signature columns to the Events and Search tab listviews. Thanks JonathanD
- Added filtering and grouping to the Events and Search tab listviews. Thanks JonathanD

**v1.1.0**

- Fixed issue where the Rules query failed when a particular sensor was chosen and a To date was selected. Thanks DannyF
- Reorganised the code e.g. moved Forms to separate form, same for Controls
- Added ability to include source and destination ports when excluding. Thanks DannyF
- Modified the Exclude window to display the protocol. The protocol is also saved with the Exclude record
- Modified the Commands.xml loading so that it does not error when the file does not exist
- Added extra checks when loading rules e.g. sensor or priority not selected
- Set the Enter key to close/accept the Acknowledgement window. Thanks MattN
- Renamed NetWitness query from “Source->Destination“ to “Src <- -> Dest”. Thanks MattN
- Added “notes” field to acknowledgement table
- Moved all SQL queries to separate files as the current Sql.xml file was getting cumbersome
- Added the ability to categorise from the Search tab. Thanks DannyF
- Added the ability to export all acknowledgements for the selected period and user (initials). (File->Export->Acknowledgements). Leave the Initials textbox blank to get all acknowledgements for the period
- Changed all “Task.Factory.StartNew” code to “new Thread”
- Added a new Misc tab. The new tab shows the sensor name, and the events SID and CID
- Wrapped Acknowledgements with a transaction so the inserts should be quicker
- Modified the Acknowledgements to prevent duplicates. Thanks ChrisB
- Added the Enter key to search when using the Find functionality from the Payload window. Thanks DannyF
- Added keyboard shortcuts to speed up acknowledging. Thanks CalG

    CTRL-ALT-F (False Positive)
    CTRL-ALT-T (Taken)

- Added keyboard shortcuts to speed up analysis

    CTRL-ALT-P (Show payload window)
    CTRL-ALT-A (Show the signature window)

- Added "Web Based Attack" item to the Acknowledgement classes data

**v1.0.16**

- Modified the acknowledge setting to ensure that no controls are accessed from the background thread

**v1.0.15**

- Fixed bug on the Search tab where the signature name couldn’t be searched. Thanks DannyF

**v1.0.14**

- Modified the Rules tab context menu to include a Signature option which will display the signature window without having to change the Event Info tab
- Updated the Sensors tab query as barnyard does not update the timestamp of the last event. Thanks DanO
- Fixed issue where the SID’s of signatures may be the same. Thanks DanO
- Added the signature GID to the Rule combox. Thanks DanO

**v1.0.13**

- Modified to allow the filtering of Rules on a per sensor basis
- Modified to allow user configurable commands to be executed via the Rules list context menu. The commands are stored in the Commands.xml file located in the user’s application data directory for the application. The command strings will have data substitutions applied using the following mark up so that event data can be passed to the commands:

    \#IP_SRC\#
    \#IP_DST\#
    \#PORT_SRC\# 
    \#PORT_DST\#
    \#PROTO\#
    \#SENSOR_ID\#


- Modified to allow better multi-user collaboration. The user can now right click on an event(s) and use the Acknowledgment context menu item to categorise the event. The user should set event(s) to “Unclassified” when initially looking into an Event, and then assign the actual category once the analysis is complete
- Modified to set use F1 key to set the current set of events to Taken

**v1.0.12**

- Modified to allow the deletion of multiple excludes in one go. Thanks DannyF
- Modified to allow the export of the current rules on the Rules tab. This is designed to make off line working/note taking easier
- Added two new NetWitness queries e.g. prior traffic to a source host, and prior traffic to a destination host. These queries will locate all traffic to the selected host for a period of two minutes before the event timestamp and one minute after

**v1.0.11**

- Modified to change mutex permissions

**v1.0.10**

- Updated NetWitness query. Thank DanO

**v1.0.9**

- Fixed the search tab so that condition combo box is showing. Thanks DannyF
- Modified the NetWitness query generate to include the NWS prefix for SSL connections

**v1.0.8**

- Fixed bug in copy functionality
- Modified the NetWitness query generation as the generated query was too complex. Thanks ChrisB
- Added Find window/functionality for use in the Payload window. Thanks ChrisB
- Added the ability to copy the Host column value via the context menu
- Added Enter key event handler to the Rule list which displays the Payload window. Escape now closes the Payload window
- Modified Find window to catch F3 to continue finding text

**v1.0.7**

- Modified the context menu displayed on the Rule tab so that the menu items are more appropriately enabled/disabled depending on what is selected
- Added new window to display the payload details. Double click the entry to display. Thanks ChrisB
- Added the ability to group the list items. Use the context menu by right clicking on the list header. Thanks MattN
- Enabled the ability to filter the list items. Use the context menu by right clicking on the list header
- Added Host column to list views (Events, Rules, Search) which is parsed from the ASCII payload. Thanks ChrisB
- Recoded all data access to use NPoco rather than Massive
- Removed the preloading of all possible signature names and ID’s for the Search facility due to performance issues on slow infrastructure
- Moved the localised rule storage from ESENT to SQL Server CE to permit the opening of multiple instances of the application
- Added Netwitness query string generation. Right click on an event, select Netwitness Query menu item. Thanks MattN

**v1.0.6**

- Corrected Exclude functionality to reverse byte order of IP addresses. Thanks ChrisB

**v1.0.5**

- Added the ability to export the current events from the Rules tab to a TSV file. Use the context menu to export
- Added the ability to export all events associated with the current ruleset/time period defined on the Rules tab to a TSV file. Use the context menu to export
- Modified the event loading to recalculate the total records loaded using the number of records loaded
- Reworking of the Hide functionality. It now uses a table within the snort database e.g. “exclude”. Thanks MattN
- Added the ability to export all Excludes to TSV for offline analysis
- Added the ability to filter the Rules by Priority. Thanks MattN
- Added alternative row colours. Thanks ChrisB

**v1.0.4**

- Fixed Rules paging label positioning/anchoring
- Added the ability to extract distinct source/destination IP addresses for a specific rule/date/date range. Access the functionality by using the context menu on an Event. This functionality applies to the Rules tab

**v1.0.3**

- Updated the rule import to prevent old rules being discarded
- Fixed bug where the Rules drop down list displayed duplicate rules
- Fixed import paths e.g. from application directory to user app data directory
- Added a default MySql connection string example when creating a new connection

**v1.0.2**

- Modified the Sensor tab to prevent it automatically loading at start up, as some instances may not be accessible. To refresh the Sensor data a new refresh button has been added to the top of the tab
- Removed the Error & Exclamation error handlers within the four main user controls as they were unnecessary

**v1.0.1**

- Modified the Rules database to store within the users local app data directory. This prevents issues when running with multiple users logged into the same time. Thanks DanO for reporting this on behalf of TomB ;-)
- Modified the Connections config file to store within the users local app data directory
- Modified the Settings config file to store within the users local app data directory
- Modified the HEX view context menu to allow copying of the HEX with and without spaces
- Added the ability to filter out particular events e.g. false positives. The functionality only applies on the Rules list. A false positive entry relates to a particular attribute e.g payload LIKE “test” or Source IP = 192.168.0.100. The false positive data is stored in an XML file under the users local app data directory. Thanks ChrisB

**v1.0.0**

- Help file added
- Public release

**v0.0.3**

- Updated the rule import to update existing rules
- Added event handlers for Sensor user control so that messages can be transmitted back to the main UI
- Modified the Event user control to just use next/previous paging as a record count takes too long
- Added the ability to search on Sensor
- Added the ability to search on Protocol (TCP, UDP and ICMP)
- Moved all of the querying to a separate object so that the queries can now be easily run on a background thread
- Increased the granularity of the Page Limits, for very slow connections!
- Moved the “Connections.xml” and “Settings.xml” to a new “Config” folder
- Moved all of the hard coded SQL queries to a new config file (“Sql.xml”). The file resides in the new “Config” folder
- Added TCP flag decoding to the events list. The events is displayed on the Event, Rules and Search tabs

**v0.0.2**

- Fixed context menu Source Port copy
- Fixed import rules error which resulted in a Disposed object exception
- Added To Date/Time filtering. Thanks TomB
- All controls/lists clear when the Rules combo is refreshed
- Fixed “No Object in Sequence” error when editing an existing connection
- Moved the Connections/Page Limit controls to the toolbar
- Added new Events tab, which displays all events, ordered by event.timestamp, includes paging support
- Rule files are now copied to the Import directory when a manual rule import is performed. This will ensure that the Settings file will contain the file details and reimport will not occur
- Add new Search tab, which allows for user configurable searching on the key fields
- Re-implemented UI code base using User Controls rather than one massive code dump in the main window
- Added custom context menu to the HEX control to allow the copying of the HEX value as well as the ASCII 
- Added Sensor tab which displays information relating to the snort sensors

**v0.0.1**
 
- Initial release

