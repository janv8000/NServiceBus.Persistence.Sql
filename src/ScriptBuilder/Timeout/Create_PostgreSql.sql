﻿create or replace function create_timeouts_table(tablePrefix varchar)
  returns integer as
  $body$
    declare
      tableNameNonQuoted varchar;
      createTable text;
    begin
        tableNameNonQuoted := tablePrefix || 'TimeoutData';
        createTable = 'CREATE TABLE IF NOT EXISTS public.' || tableNameNonQuoted || '
    (
        "Id" character varying(38) NOT NULL,
        "Destination" character varying(200),
        "SagaId" character varying(38),
        "State" bytea,
        "Time" timestamp with time zone,
        "Headers" text,
        "PersistenceVersion" character varying(23),
        PRIMARY KEY ("Id")
    )
    WITH (
        OIDS = FALSE
    );
    CREATE INDEX IF NOT EXISTS "Time_Idx" ON public.' || tableNameNonQuoted || ' USING btree ("Time" ASC NULLS LAST);
    CREATE INDEX IF NOT EXISTS "SagaId_Idx" ON public.' || tableNameNonQuoted || ' USING btree ("SagaId" ASC NULLS LAST);
';
		execute createTable;
        return 0;
    end;
  $body$
  language 'plpgsql';

select create_timeouts_table(@tablePrefix);