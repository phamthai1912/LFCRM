Create procedure DeleteProduction

@productionid int
as
begin

DELETE FROM MatHang WHERE ID_NhaSanXuat=@productionid
DELETE FROM NhaSanXuat WHERE ID_NhaSanXuat=@productionid

end


