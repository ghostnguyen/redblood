truncate table PackStatusHistory
truncate table PackResultHistory
truncate table PackOrder
truncate table PackExtract

truncate table BloodType
truncate table TestResult

delete Pack
DBCC CHECKIDENT (Pack, RESEED, 0)

delete [Order]
DBCC CHECKIDENT ([Order], RESEED, 0)

truncate table CampaignStatusHistory

delete [Campaign]
DBCC CHECKIDENT ([Campaign], RESEED, 0)

truncate table PeopleStayIn
delete [People]

delete Org
DBCC CHECKIDENT ([Org], RESEED, 0)
truncate table [Log]