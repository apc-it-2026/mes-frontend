SELECT
A.documents AS '订单号',
A.packing_barcode AS '条码',
A.lot_barcode AS '批号',
REPLACE(A.material_no,',','') AS '品号',
A.material_name AS '品名',
CASE  WHEN len(REPLACE(A.material_specifications,',',''))>15 THEN left(REPLACE(A.material_specifications,',',''),15) ELSE REPLACE(A.material_specifications,',','') END  AS '规格',
A.qty AS '重量',
A.createdate AS '日期',
A.createby AS '工号',
REPLACE(B.custom_material_no,',','') AS '客户品号',
REPLACE(B.custom_location,',','') AS '客户库位',
job_number AS '表面处理'
FROM CODE003M A
LEFT JOIN BASE007D3 B ON A.material_no = B.material_no AND A.suppliers_lot = B.custom_no



