USE GestionTareas;
GO

-- 1. JSON_VALUE: scalar values
-- High priority tasks
SELECT Id, Title, JSON_VALUE(AdditionalInfo, '$.priority') AS Priority
FROM Tasks
WHERE JSON_VALUE(AdditionalInfo, '$.priority') = 'High';

-- 2. JSON_QUERY: objects or arrays
-- JSON_VALUE returns NULL for arrays or objects; JSON_QUERY returns them whole
SELECT Id, Title,
       JSON_VALUE(AdditionalInfo, '$.tags') AS TagsWithValue,
       JSON_QUERY(AdditionalInfo, '$.tags') AS TagsWithQuery
FROM Tasks
WHERE JSON_QUERY(AdditionalInfo, '$.tags') IS NOT NULL;

-- 3. OPENJSON: tags array to rows
-- Distinct tags in use
SELECT DISTINCT tag.value
FROM Tasks t
CROSS APPLY OPENJSON(t.AdditionalInfo, '$.tags') AS tag;

-- Tasks with a given tag
SELECT t.Id, t.Title, tag.value AS Tag
FROM Tasks t
CROSS APPLY OPENJSON(t.AdditionalInfo, '$.tags') AS tag
WHERE tag.value = 'auth';

-- Task count per tag
SELECT tag.value AS Tag, COUNT(*) AS TaskCount
FROM Tasks t
CROSS APPLY OPENJSON(t.AdditionalInfo, '$.tags') AS tag
GROUP BY tag.value
ORDER BY TaskCount DESC;

-- 4. JSON_MODIFY: update the JSON
-- Change the priority and append a tag (rolled back at the end)
BEGIN TRAN;

UPDATE Tasks
SET AdditionalInfo = JSON_MODIFY(AdditionalInfo, '$.priority', 'Low')
WHERE Id = 1;

UPDATE Tasks
SET AdditionalInfo = JSON_MODIFY(AdditionalInfo, 'append $.tags', 'backend')
WHERE Id = 1;

SELECT Id, AdditionalInfo FROM Tasks WHERE Id = 1;

ROLLBACK;

-- 5. CHECK: reject invalid JSON
-- Expected to fail with error 547 (CK_Tasks_AdditionalInfo_IsJson)
BEGIN TRAN;

UPDATE Tasks
SET AdditionalInfo = 'This is not a Json'
WHERE Id = 1;

ROLLBACK;
