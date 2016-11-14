msbuild /p:Configuration=Release BackToTheFuture.sln
msbuild /p:Configuration=Release CreateLayout.prj

makeappx pack /o /d RidoClassicApp.Appx.\Layout /p RidoClassicAppStep1.appx
signtool sign /fd SHA256 /a /v RidoClassicAppStep1.appx
