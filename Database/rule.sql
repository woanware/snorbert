CREATE TABLE rule (
  sig_name VARCHAR(255) NOT NULL,
  sig_rev INT UNSIGNED,
  sig_sid INT UNSIGNED,
  sig_gid INT UNSIGNED,
  rule VARCHAR(1000) NOT NULL,
  PRIMARY KEY (sig_name, sig_rev, sig_sig, sig_gid)
);
