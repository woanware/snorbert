snorbert
========

snorbert is a [snort](http://www.snort.org/) data viewer, loosely based on [snorby](https://snorby.org/). It is written in C# and uses .Net 4.5.

The aim of the project is to provide a fast, usable interface for accessing snort data. Depending on the snort deployment, the underlying data set can be extremely large, so some time has been taken to optimise the data access.

snorbert has a number of useful features:

- Paged data access
- Configuration for multiple snort instances
- Rule/signature based grouping of events
- User configurable searching
- Correlation of snort signatures to events for easy viewing of the signatures

snorbert uses a number of third party libraries:


- [ObjectListView](http://objectlistview.sourceforge.net/cs/index.html) : Data viewing via lists 
- [Be.HexEditor](http://sourceforge.net/projects/hexbox/) : HEX view of packet data
- [IP Address Control](http://www.codeproject.com/Articles/9352/A-C-IP-Address-Control) : Easy validation of IP addresses
- [ManagedEsent](http://managedesent.codeplex.com/) : Fast storage of rule data
- [MySql](http://dev.mysql.com/downloads/connector/net/) : Access to snort mysql database
- [Utility](http://www.woanware.co.uk) (woanware) : My helper library