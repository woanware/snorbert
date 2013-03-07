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

## Third party libraries ##

- [ObjectListView](http://objectlistview.sourceforge.net/cs/index.html) : Data viewing via lists 
- [Be.HexEditor](http://sourceforge.net/projects/hexbox/) : HEX view of packet data
- [IP Address Control](http://www.codeproject.com/Articles/9352/A-C-IP-Address-Control) : Easy validation of IP addresses
- [ManagedEsent](http://managedesent.codeplex.com/) : Fast storage of rule data
- [MySql](http://dev.mysql.com/downloads/connector/net/) : Access to snort MySQL databases
- [Utility](http://www.woanware.co.uk) (woanware) : My helper library
- [NPoco](https://github.com/schotime/NPoco): Data access
- [SQL Server](http://www.microsoft.com/en-gb/download/details.aspx?id=30709): SQL Server CE used for rule storage
- [CsvHelper](https://github.com/JoshClose/CsvHelper): CSV output

## Requirements ##

- Microsoft .NET Framework v4.5
- snort/barnyard database change (see below)


## Database ##
snorbert requires a change to the snort/barnyard database schema. Currently the change simply consists of one new table (Exclude). To add the new table just run the Create.sql file under the database directory of the repository. The table facilities the ability to exclude particular rules, IP addresses etc. 