<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorMsg.aspx.cs" Inherits="WMS.ErrorMsg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="600" border="0" align="center" cellpadding="5" cellspacing="0">
					<tr>
						<td  class="table_bgcolor">
							<table width="100%"  cellpadding="5" cellspacing="0" 
							
							class="table_bordercolor"
							
							>
								<tr bgcolor="#e4e4e4">
									<td height="22" class="table_titlebgcolor"><STRONG><FONT color="red">错误信息</FONT></STRONG></td>
								</tr>
								<tr>
									<td height="22">
										<table  width="100%">
											<tr>
												<td height="22">
													<asp:Label id="lblMsg" runat="server" Width="100%"></asp:Label>
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td height="22">
										<div align="center"><input type="button" value="返回" style="WIDTH: 120px" onclick="javascript:history.back();"></div>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
    </div>
    </form>
</body>
</html>
