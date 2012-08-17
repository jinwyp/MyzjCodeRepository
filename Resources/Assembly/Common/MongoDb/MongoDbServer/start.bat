
@echo

@pause

bin\mongod -repair -dbpath data
bin\mongod -dbpath data

@pause