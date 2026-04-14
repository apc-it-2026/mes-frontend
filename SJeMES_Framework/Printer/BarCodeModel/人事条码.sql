SELECT 
staff_no AS '条码',	
staff_name AS '姓名',
CASE staff_sex WHEN 'Male' THEN '男' ELSE '女' END	AS '性别',
(select department_name from BASE005M where department_code = HR001M.staff_department) AS '所属部门',
staff_post AS '职位',
CASE staff_status WHEN '1' THEN '在职' WHEN '2' THEN '离职' WHEN '3' THEN '试用期' ELSE '实习' END	AS '状态'
from HR001M(NOLOCK)
 

