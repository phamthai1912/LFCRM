CREATE PROCEDURE ImportProduct
	@idphieunhap int, 
	@idnhacungcap int, 
	@idnguoidung int, 
	@ngaynhap datetime, 
	@idmathang int, 
	@soluong int, 
	@dongia int

AS
BEGIN

	INSERT INTO PhieuNhap(ID_PhieuNhap, ID_NhaCungCap, ID_NguoiDung, NgayNhap) 
		VALUES(@idphieunhap, @idnhacungcap, @idnguoidung, @ngaynhap)
	
	INSERT INTO ChiTietPhieuNhap(Id_PhieuNhap, Id_Mathang, SoLuong, DonGia) 
		VALUES (@idphieunhap, @idmathang, @soluong, @dongia)

END

