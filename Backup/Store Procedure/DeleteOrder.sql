CREATE procedure DeleteOrder

@orderid int
as
begin

DELETE FROM ChiTietDonHang WHERE ID_DonHang=@orderid
DELETE FROM DonHang WHERE ID_DonHang=@orderid

end

