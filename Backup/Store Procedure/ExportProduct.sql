CREATE PROCEDURE ExportProduct
	@idphieuxuat int, 
	@idnguoidung int, 
	@ngayxuat datetime, 
	@idproduct int, 
	@quantity int, 
	@notes nvarchar(max)

AS
BEGIN

	INSERT INTO PhieuXuat(ID_PhieuXuat, ID_NguoiDung, NgayXuat) 
		VALUES(@idphieuxuat, @idnguoidung, @ngayxuat)
	
	INSERT INTO ChiTietPhieuXuat(Id_PhieuXuat, Id_Mathang, SoLuong, GhiChu) 
		VALUES (@idphieuxuat, @idproduct, @quantity, @notes)

END