# The architecture of GitInsight

```plantuml
@startuml
() "DefaultMode" as commit
() "AuthorMode" as author
() "dotnet test" as dotnettest

package "GitInsight" {
  [Program] as program
  [LibGit2Sharp] as lb2s

  [GitInsight.Tests] as test

  () "CommitAnalyzer" as commitanalyzer
  () "Context" as context

  () "AnalyzedRepoRepository" as analyzedrepo
  () "DataCommitRepository" as datarepo
  
  database "TestDB" as testdb {

  }

  database "GitInsightDB" as db {
    [AnalyzedRepo]
    [DataCommit]
  }
}

test --> testdb
testdb --> context

program --> commitanalyzer
commitanalyzer --> analyzedrepo

analyzedrepo --> db
db --> lg2s
datarepo --> db

dotnettest --> test
commit --> program
author --> program

db --> lb2s

program --> () output
@enduml
```

![Component Diagram](//www.plantuml.com/plantuml/png/NLB1JiCm3BtxAwoUu30E-m34O1920Wa1jsa7RmbTHTjKYIin9lwTECxMXafLu_TUdf_JN5rZjRQVDBRyueoja0BkNTSr34AjHZuuVs4ebOanZdlBo3PmehIM4c2W_HVL5gl7Xf_wqDI7g3a1ukRLkzfJjvKTWwu5VcvsKRfyFv0V4jVMom32J0ut7z4x95Qx84gEXnHsrLDx_B5-bfyoTD7rRBzP1Re-M-JDviqEJjxGiFD77JXJFi9PlIOcRVfFQoAHTO2fsb6mM4dkzNrr3cvsU9BElr2efa6pDkamFfjvlgr2KuXjiefcuesBnMrk0leHP9et7xc2vIcLfIZAuM5U9JhsK7_zMph-n87UY2AMTj4wPm6zKtEeo2bdxaOUHeOxsvknQ_y0)
