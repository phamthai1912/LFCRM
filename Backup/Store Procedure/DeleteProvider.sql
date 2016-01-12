Create procedure DeleteProvider
@providerid int
as
begin

DELETE FROM PhieuNhap WHERE ID_NhaCungCap=@providerid
DELETE FROM NhaCungCap WHERE ID_NhaCungCap=@providerid

end
