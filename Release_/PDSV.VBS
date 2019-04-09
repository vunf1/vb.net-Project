Set objShell = WScript.CreateObject("WScript.Shell")
dim strDir

'スクリプトがあるフォルダを取得
strDir = Replace(WScript.ScriptFullName, WScript.ScriptName,"")

'EXEはスクリプトと同じフォルダにある想定
strDir = strDir & "PDSV.EXE /Agree"

'EXE実行
objShell.Run strDir
Set objShell = Nothing

