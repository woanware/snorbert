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
- [IP Address Control](http://www.codeproject.com/Articles/9352/A-C-IP-Address-Control) : Easy validation of IP addresses
- [SQL Server CE](http://www.microsoft.com/en-gb/download/details.aspx?id=30709): SQL Server CE used for rule storage
- [MySql](http://dev.mysql.com/downloads/connector/net/) : Access to snort MySQL databases
- [NPoco](https://github.com/schotime/NPoco): Data access
- [ObjectListView](http://objectlistview.sourceforge.net/cs/index.html) : Data viewing via lists 
- [Utility](http://www.woanware.co.uk) (woanware) : My helper library


## Requirements ##

- Microsoft .NET Framework v4.5
- snort/barnyard database change (see below)

## Database ##
snorbert requires a number of changes to the snort/barnyard database schema. The following files should be run to create new tables:

- Database\acknowledgment.sql 
- Database\acknowledgment_class.sql
- Database\exclude.sql

Then the data population script (acknowledgment_class.data.sql) should be run to populate the **acknowledgment_class** table. The **exclude** table facilities the ability to exclude particular rules, IP addresses etc. The **acknowledgement*** tables allow for better collaborative working so that one analyst can see that another analyst is already working on a particular rule.