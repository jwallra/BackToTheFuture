makeappx pack /o /d Layout /p RidoClassicAppStep1.appx
signtool sign /fd SHA256 /a /v RidoClassicAppStep1.appx
