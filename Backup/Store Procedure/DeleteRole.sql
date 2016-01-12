Create procedure DeleteRole
@roleid int
as
begin

DELETE FROM NguoiDung WHERE ID_Quyen=@roleid
DELETE FROM PhanQuyen WHERE ID_Quyen=@roleid

end
