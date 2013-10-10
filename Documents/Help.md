snorbert
========

snorbert is a [snort](http://www.snort.org/) data viewer, loosely based on [snorby](https://snorby.org/). It is written in C# and uses .Net 4.5.

The aim of the application is to provide a fast, usable interface for accessing snort data. Depending on the snort deployment, the underlying data set can be extremely large, so care has been taken to optimise the data access. snorbert has various useful features:

## Features ##

- Paged data access
- Configuration for multiple snort instances
- Signature based grouping of events
- User configurable searching
- Correlation of snort signatures to events for easy viewing of the signatures
- Query integration with NetWitness for quick session identification

## Third party libraries ##

- [CsvHelper](https://github.com/JoshClose/CsvHelper): CSV output
- [Be.HexEditor](http://sourceforge.net/projects/hexbox/) : HEX view of packet data
- [IP Address Control](https://code.google.com/p/ipaddresscontrollib/) : Easy validation of IP addresses
- [MySql](http://dev.mysql.com/downloads/connector/net/) : Access to snort MySQL databases
- [NPoco](https://github.com/schotime/NPoco): Data access
- [ObjectListView](http://objectlistview.sourceforge.net/cs/index.html) : Data viewing via lists 
- [Utility](http://www.woanware.co.uk) (woanware) : My helper library

## Requirements ##

- Microsoft .NET Framework v4.0
- snort/barnyard database change (see below)

## Database ##
snorbert requires a number of changes to the snort/barnyard database schema. The following files should be run to create new tables:

- Database\acknowledgment.sql 
- Database\acknowledgment_class.sql
- Database\exclude.sql
- Database\rule.sql

Then the data population script (acknowledgment\_class.data.sql) should be run to populate the **acknowledgment_class** table. The exclude table facilities the ability to exclude particular rules, IP addresses etc. The **acknowledgement** tables allow for better collaborative working so that one analyst can see that another analyst is already working on a particular rule.

To use the "rule" functionality you must run the import.py python script which will populate the "rule" table. This can be implemented as a cron job or whenever your rules have changed

# Usage #

## Connections ##

snorbert can connect to multiple snort instances. The database connections need to be defined for each snort instance. The database connections can be configured via the Tools->Connections menu. The Connections window will display all of the configured snort databases.

![](Connections.png)
 
The Connections window allows the adding, editing and deleting of database connections. The Connection window is shown below:

![](Connection.png)
 
The ellipsis button will perform a connection test for the currently configured connection string. The connection string must be in the following format:
Data Source=#IP#;Initial Catalog=#Database#; User Id=#username#;password=#password#; default command timeout=60;

Note that the default command timeout can be configured for each instance, this allows for slow network connections, large datasets etc.

## Rules/Signatures ##

The snort rule set can be imported into snorbert, this allows the signature/rule to be displayed that relates to a specific event. The screenshot below shows the signature details:

![](Rules.png)
 
To view the full rule in a separate window, click the button next to the rule, the following window will be displayed:

![](Rule.png)
 
The rules can be imported manually via the Tools->Import Rules menu or they can be copied into the Rules directory located in the applications installation directory. The automated import will check the file names/timestamps and only import new or changed files.

## False Positives ##

To help reduce the amount of “noise” returned by snort it is possible to aid false positive filters to the data set. The false positive filters are only application to the Rules tab since they relate a filter to a specific snort rule.

To add a false positive filter, load the events for a rule, then right click on the Event line within the list. A context menu will be displayed, choose the Hide item. The following window will be displayed:
 
![](False.Positive.png)

The values will be pre-populated using the event selected in the list; this is to speed up the process. To remove false positive filters, use the Tools->False Positive menu item, which will display the False Positives window:

![](False.Positives.png)
 
Select the false positive filter that you want to delete and use the delete button on the window or use the DEL key.

## Alerts ##

snobert has the ability to alert the user when important events have triggered. The Alerts are configured via the Alerts.xml file that should be placed in the users AppData directory for the application e.g.

    C:\:\Users\woanware\AppData\Local\woanware\snorbert\Alerts.xml

The format of the config file is shown below:

    <?xml version="1.0" encoding="utf-8"?>
    <Alerts xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
      <Interval>60</Interval>
      <Priorities>
        <int>1</int>
      </Priorities>
      <Keywords>
	    <string>[BADSTUFF]</string>
      </Keywords>
    </Alerts> 

The Interval (minutes) value is used to configure an internal timer which is used to perform the check in the background. The Priorities section is used to define any priority signatures that need to be alerted. The Keywords section is used to alert on signatures that have specific keywords in the signature name.

When events are identified that match any alerting criteria a message box is displayed and the applications window is flashed. The rule which has the new events against it will have red text. The red text will only be shown until the next automatic refresh/check