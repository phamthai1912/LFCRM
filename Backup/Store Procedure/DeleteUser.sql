Create procedure DeleteUser

@userid int
as
begin

DELETE FROM FeedBack WHERE ID_NguoiDung=@userid
DELETE FROM PhieuXuat WHERE ID_NguoiDung=@userid
DELETE FROM DonHang WHERE ID_NguoiDung=@userid
DELETE FROM NguoiDung WHERE ID_NguoiDung=@userid

end


