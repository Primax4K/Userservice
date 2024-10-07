namespace WebApi.Controllers.Abstract;

public abstract class AController<TEntity, TCreateEntityDto, TReadEntityDto, TUpdateEntityDto>(
	IRepository<TEntity> repository,
	ILogger<AController<TEntity, TCreateEntityDto, TReadEntityDto, TUpdateEntityDto>> logger) : ControllerBase
	where TEntity : class
	where TCreateEntityDto : class
	where TUpdateEntityDto : class
	where TReadEntityDto : class {
	
	[Authorize]
	[HttpGet("{id:int}")]
	public virtual async Task<ActionResult<TReadEntityDto>> ReadAsync(int id, CancellationToken ct) {
		try {
			TEntity? data = await repository.ReadAsync(id, ct);

			if (data is null) {
				logger.LogInformation($"Invalid Request: Entity not present - {id}");
				return NotFound();
			}

			logger.LogInformation($"Sending Entity: {id}");
			return Ok(data.Adapt<TReadEntityDto>());
		}
		catch (OperationCanceledException) {
			logger.LogError("Zeitüberschreitung der Anforderungen!");
			return StatusCode(408);
		}
		catch (Exception e) {
			logger.LogError(e, "Fehler beim Abrufen der Entität!");
			return Problem("Fehler beim Abrufen der Entität!");
		}
	}

	[Authorize]
	[HttpPut("{id:int}")]
	public virtual async Task<ActionResult> UpdateAsync(int id, TUpdateEntityDto record, CancellationToken ct) {
		try {
			TEntity? data = await repository.ReadAsync(id, ct);

			if (data is null) {
				logger.LogInformation($"Invalid Request: Entity not present - {id}");
				return NotFound();
			}

			await repository.UpdateAsync(record.Adapt<TEntity>(), ct);
			logger.LogInformation($"Updated Entity: {id}");

			return NoContent();
		}
		catch (OperationCanceledException) {
			logger.LogError("Zeitüberschreitung der Anforderungen!");
			return StatusCode(408);
		}
		catch (Exception e) {
			logger.LogError(e, "Fehler beim Abrufen der Entität!");
			return Problem("Fehler beim Abrufen der Entität!");
		}
	}

	[Authorize]
	[HttpDelete("{id:int}")]
	public virtual async Task<ActionResult> DeleteAsync(int id, CancellationToken ct) {
		try {
			TEntity? data = await repository.ReadAsync(id, ct);

			if (data is null) {
				return NotFound();
			}

			await repository.DeleteAsync(data, ct);
			logger.LogInformation($"Deleted Entity: {id}");

			return NoContent();
		}
		catch (OperationCanceledException) {
			logger.LogError("Zeitüberschreitung der Anforderungen!");
			return StatusCode(408);
		}
		catch (Exception e) {
			logger.LogError(e, "Fehler beim Abrufen der Entität!");
			return Problem("Fehler beim Abrufen der Entität!");
		}
	}

	[Authorize]
	[HttpPost]
	public virtual async Task<ActionResult<TReadEntityDto>> CreateAsync(TCreateEntityDto entity, CancellationToken ct) {
		try {
			return Ok((await repository.CreateAsync(entity.Adapt<TEntity>(), ct)).Adapt<TReadEntityDto>());
		}
		catch (OperationCanceledException) {
			logger.LogError("Zeitüberschreitung der Anforderungen!");
			return StatusCode(408);
		}
		catch (Exception e) {
			logger.LogError(e, "Fehler beim Abrufen der Entität!");
			return Problem("Fehler beim Abrufen der Entität!");
		}
	}

	[Authorize]
	[HttpGet("search")]
	public async Task<ActionResult<List<TReadEntityDto>>> SearchAsync(string? searchString, CancellationToken ct) {
		try {
			var countries = await repository.SearchAsync(searchString, ct);
			return countries.Select(c => c.Adapt<TReadEntityDto>()).ToList();
		}
		catch (OperationCanceledException) {
			logger.LogError("Zeitüberschreitung der Anforderungen!");
			return StatusCode(408);
		}
		catch (Exception e) {
			logger.LogError(e, "Fehler beim Abrufen der Entität!");
			return Problem("Fehler beim Abrufen der Entität!");
		}
	}
}