#!/usr/bin/python
import MySQLdb
import sys
import re
import argparse
import traceback

SQL_SELECT = """SELECT sig_name, sig_rev, sig_sid, sig_gid, rule FROM rule WHERE sig_name = %s AND sig_rev = %s AND sig_sid = %s AND sig_gid = %s"""
SQL_INSERT = """INSERT INTO rule(sig_name, sig_rev, sig_sid, sig_gid, rule) VALUES (%s, %s, %s, %s, %s)"""
SQL_UPDATE = """UPDATE rule SET rule.rule = %s WHERE sig_name = %s AND sig_rev = %s AND sig_sid = %s AND sig_gid = %s"""

def import_rules(server, database, user, password, rules_file, import_mode):
	db = MySQLdb.connect(host=server, 
						 user=user, 
						 passwd=password, 
						 db=database) 
						 
	cursor = db.cursor()					
						 
	if import_mode == 'bulk':
		db.query("""TRUNCATE TABLE `rule`""")
	with open(rules_file,"rb") as FILE:

		name = ''
		rev = 0
		sid = 0
		gid = 0
		
		for line in FILE:
			match = re.search( r'msg:\s*?"(.*?)";', line, re.I)
			if match:
				name = match.group(1)
				
			if len(name.strip()) == 0:
				continue
				
			match = re.search( r'rev:\s*?(.*?);', line, re.I)
			if match:
				rev = match.group(1)
			else:
				rev = 1
				
			match = re.search( r'sid:\s*?(.*?);', line, re.I)
			if match:
				sid = match.group(1)
			else:
				continue
				
			match = re.search( r'gid:\s*?(.*?);', line, re.I)
			if match:
				gid = match.group(1)
			else: 
				gid = 1
				
			try:
				if import_mode == 'bulk':
					# In bulk mode we truncate the table and insert all known rules
					cursor.execute(SQL_INSERT, (name.strip(), int(rev), int(sid), int(gid), line.strip()))
					continue
				elif import_mode == 'sync':
					# Retrieve any existing records
					cursor.execute(SQL_SELECT, (name.strip(), rev, sid, gid))
					rows = cursor.fetchall()
				
					# If the row does not exist then insert it else update the rule field
					if len(rows) == 0:
						print 'inserting'
						cursor.execute(SQL_INSERT, (name.strip(), int(rev), int(sid), int(gid), line.strip()))
					else:
						for row in rows:
							if row[4].strip() == line.strip():
								continue
						
							print 'Updating rule: ' + sid
							print 'From: ' + row[4].strip()
							print 'To: ' + line.strip()
						
							cursor.execute(SQL_UPDATE, (line.strip(), name.strip(), int(rev), int(sid), int(gid)))
			except Exception as err:
				print err
				print traceback.format_exc()
				print line
			   #pass

		db.commit()
		db.close()
	
###### ENTRY POINT #####################################################################################

parser = argparse.ArgumentParser(description='Imports rules into the barnyard/snort database')
parser.add_argument('--server', '-s', help='Host', nargs=1)
parser.add_argument('--user', '-u', help='User', nargs=1)
parser.add_argument('--password', '-p', help='Password', nargs=1)
parser.add_argument('--database', '-d', help='Database', nargs=1)
parser.add_argument('--file', '-f', help='Rules File', nargs=1)
parser.add_argument('--mode', '-m', help='Import mode', nargs=1)
args = parser.parse_args()

# All of the database details can be hard coded if required or passed in as command line args
server = ''
user = ''
database = ''
password = ''
file = ''
mode = 'sync'


if not args.server == None:
	server = args.server[0]
	
if not args.user == None:
	user = args.user[0]
	
if not args.database == None:
	database = args.database[0]
	
if not args.password == None:
	password = args.password[0]
	
if not args.file == None:
	file = args.file[0]

if not args.mode == None:
	mode = args.mode[0]
	
if len(server) == 0:
	print 'Server not set'
	sys.exit(1)
	
if len(database) == 0:
	print 'Database not set'
	sys.exit(1)
	
if len(user) == 0:
	print 'User not set'
	sys.exit(1)
	
if len(password) == 0:
	print 'Password not set'
	sys.exit(1)
	
if len(file) == 0:
	print 'File not set'
	sys.exit(1)

if len(mode) == 0:
    print 'Import mode not set'
    sys.exit(1)

	
import_rules(server, database, user, password, file, mode)
