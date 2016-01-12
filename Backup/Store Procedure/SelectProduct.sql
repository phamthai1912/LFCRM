set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[SelectProduct]

as
begin

select ID_MatHang,TenMatHang,LoaiHang,NhaSanXuat,HinhAnh,DonGia,SoLuong,MoTa
from MatHang,NhaSanXuat,LoaiHang 
Where MatHang.ID_LoaiHang = LoaiHang.ID_LoaiHang and MatHang.ID_NhaSanXuat = NhaSanXuat.ID_NhaSanXuat

end

