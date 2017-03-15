VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "MyClass"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Public Function Salute(ByVal name As String) As String
    Dim result As String
    result = "Hello " & name
    Salute = result
End Function

Public Function GetInfo() As String
    Dim info As String
    info = App.Path & " " & App.EXEName & " " & App.Major & App.Minor & App.Revision
    GetInfo = info
End Function

Public Sub MakeMeFail()
    Err.Raise 888, "ClassicCOM.MakeMeFail", "Fake Error to test creash reporting"
End Sub
