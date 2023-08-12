### File Templates

`ild`  
```
[InlineData($END$)]
```  
  
`theory`
```
    [Theory]
    public async Task $methodName$(){
        $END$
    }
```

`arr`
```
new[] {$END$}
```