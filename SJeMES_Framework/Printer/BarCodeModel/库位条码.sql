SELECT 
location_no AS '库位代号',
location_name AS '库位说明',
warehouse_name AS '库名',
'' AS '库址',
location_no AS '条码'
FROM BASE011M(NOLOCK)
JOIN BASE010M(NOLOCK) ON BASE011M.warehouse =BASE010M.warehouse_no
