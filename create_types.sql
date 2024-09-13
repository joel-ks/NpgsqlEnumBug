-- Run this first to create the required DB types
create type test_enum as enum ('value1', 'value2', 'value3');
create type test_udt as (enum_value test_enum);