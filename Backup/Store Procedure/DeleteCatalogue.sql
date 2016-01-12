CREATE procedure DeleteCatalogue

@catalogueid int
as
begin

DELETE FROM MatHang WHERE ID_LoaiHang=@catalogueid
DELETE FROM LoaiHang WHERE ID_LoaiHang=@catalogueid

end

